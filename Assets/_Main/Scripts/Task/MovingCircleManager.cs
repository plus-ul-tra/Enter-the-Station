using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
//using UnityEngine.Splines.ExtrusionShapes;
//using System.Net;
//using UnityEngine.InputSystem.iOS;
//using System.Runtime.CompilerServices;


public class MovingCircleManager : Task
{
    private MovingScope movingScope;
    private MovingCircle movingCircle;

    [Header("난이도 조정")]
    public float scopeScaleX;
    public float circleSpeed;
    public float scopeSpeed;

    public UnityEvent Initial;
    public UnityEvent SetAllStop;

    bool isSuccess;
    bool isClose;

    [SerializeField] private MovingScope scope;
    [SerializeField] private MovingCircle circle;

    [Header("회전시킬 UI Image")]
    [SerializeField] private Image playerImage;

    [Header("원하는 Z축 회전 각도(도 단위)")]
    [SerializeField] private float deltaAngle = 10f;

    [Header("변경할 순서대로 Sprite들")]
    public Sprite[] sprites;

    private float currentAngle = 0f;

    [Header("제어할 UI Image")]
    public Image targetImage;
    
    private int currentIndex = 0;//적 애니매이션 바뀌는 횟수
    private int count = 1;//성공횟수
    private void Start()
    {
        if (playerImage == null)
        {
            playerImage = GetComponent<Image>();
            if (playerImage == null)
            {
                enabled = false;
                return;
            }
        }

        if (sprites == null || sprites.Length == 0)
        {
            enabled = false;
            return;
        }
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
            if (targetImage == null)
            {
                enabled = false;
                return;
            }
        }

        // 초기 스프라이트 세팅
        targetImage.sprite = sprites[currentIndex];

        movingScope = scope.GetComponent<MovingScope>();
        movingScope.Balance();
        movingCircle = circle.GetComponent<MovingCircle>();
        movingCircle.Balance();
    }


    void OnEnable()
    {
        targetImage.sprite = sprites[currentIndex];
        InitGame();
        Initial.Invoke();
    }
    public override void InitGame()
    {
        successImage.SetActive(false);
        failedImage.SetActive(false);
        timer = 0.0f;
        isClose = false;

        //isSuccess = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !isClose)
        {
            scope.RandomizeStartX(-250f, 250f);//범위사이에 랜덤값으로 스코프 이동

            if (isSuccess)
            {
                SoundManager.Instance.PlaySFX("Map_finish");
                currentIndex = (currentIndex + 1) % sprites.Length;
                targetImage.sprite = sprites[currentIndex];
                currentAngle -= deltaAngle;
                count++;        //성공횟수 카운트        
            }
            else if (!isSuccess)
            {
                if (currentIndex - 1 >= 0)
                {
                    SoundManager.Instance.PlaySFX("Fail_sound");
                    currentIndex = (currentIndex - 1) % sprites.Length;
                    targetImage.sprite = sprites[currentIndex];
                }
                currentAngle += deltaAngle;
                count--;        //성공횟수 카운트
            }

            if (count >= 4)// 성공시 실행 로직
            {
                circle.SetSmile();
                CountManager.Instance.AddClearCount();
                successImage.SetActive(true);
                isClose = true;
                Close();
                Reset();
            }
            else if (count <= 0)//실패시 실행 로직
            {
                if (stageManager != null)
                    stageManager.DecreasePlayerHp();
                failedImage.SetActive(true);
                isClose = true;
                Close();
                Reset();
            }
            ApplyRotation(currentAngle);
            Debug.Log(currentIndex);
        }

        //타임오버
        if (timer >= limitTime && !isSuccess)
        {
            if (stageManager != null)
                stageManager.DecreasePlayerHp();
            failedImage.SetActive(true);
            isClose = true;
            Close();
            Reset();
        }

        if (isClose) { SetAllStop.Invoke(); }
    }

    private void Reset()
    {
        timer = 0.0f;
        count = 1;
        currentIndex = 0;
        currentAngle = 0f;
    }
    public void ApplyRotation(float angle)
    {
        playerImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle + 60);
    }
    public void SetSuccess() { isSuccess = true; }
    public void SetFail() { isSuccess = false; }
}
