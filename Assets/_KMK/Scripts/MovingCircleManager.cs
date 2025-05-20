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
    public UnityEvent Initial;
    public UnityEvent SetAllStop;

    bool isSuccess;
    bool isClose;

    [SerializeField] private MovingScope scope;
    [SerializeField] private MovingCircle circle;

    [Header("ȸ����ų UI Image")]
    [SerializeField] private Image playerImage;

    [Header("���ϴ� Z�� ȸ�� ����(�� ����)")]
    [SerializeField] private float deltaAngle = 10f;

    [Header("������ ������� Sprite��")]
    public Sprite[] sprites;

    private float currentAngle = 0f;

    [Header("������ UI Image")]
    public Image targetImage;

    private int currentIndex = 0;//�� �ִϸ��̼� �ٲ�� Ƚ��
    private int count;//����Ƚ��
    private void Start()
    {
        count = 1;

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

        // �ʱ� ��������Ʈ ����
        targetImage.sprite = sprites[currentIndex];
    }


    void OnEnable()
    {
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
            scope.RandomizeStartX(-250f, 250f);//�������̿� ���������� ������ �̵�

            if (isSuccess)
            {
                currentIndex = (currentIndex + 1) % sprites.Length;
                targetImage.sprite = sprites[currentIndex];
                currentAngle -= deltaAngle;
                count++;        //����Ƚ�� ī��Ʈ        
            }
            else if (!isSuccess)
            {
                if (currentIndex - 1 >= 0)
                {
                    currentIndex = (currentIndex - 1) % sprites.Length;
                    targetImage.sprite = sprites[currentIndex];
                }
                currentAngle += deltaAngle;
                count--;        //����Ƚ�� ī��Ʈ
            }

            if (count >= 4)// ������ ���� ����
            {
                circle.SetSmile();
                successImage.SetActive(true);
                isClose = true;
                Close();
                timer = 0.0f;
            }
            else if (count <= 0)//���н� ���� ����
            {
                if (stageManager != null)
                    stageManager.DecreasePlayerHp();
                failedImage.SetActive(true);
                isClose = true;
                Close();
            }
            ApplyRotation(currentAngle);
        }

        //Ÿ�ӿ���
        if (timer >= limitTime && !isSuccess)
        {
            if (stageManager != null)
                stageManager.DecreasePlayerHp();
            failedImage.SetActive(true);
            isClose = true;
            Close();
            timer = 0.0f;
        }

        if (isClose) { SetAllStop.Invoke(); }
    }
    public void ApplyRotation(float angle)
    {
        playerImage.rectTransform.localRotation = Quaternion.Euler(0f, 0f, angle + 60);
    }
    public void SetSuccess() { isSuccess = true; }
    public void SetFail() { isSuccess = false; }
}
