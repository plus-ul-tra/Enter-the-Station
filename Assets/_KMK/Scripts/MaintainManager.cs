using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class MaintainManager : BaseGauge
{
    BoxCollider2D gaugeCollider;
    public GameObject movingGaugeCollider;

    public UnityEvent SetAllStop;

    [SerializeField]
    private GameObject heart;
    bool isReached;

    float colliderPosY;
    void OnEnable()
    {
        InitGame();
    }
    public override void InitGame()
    {
        successImage.SetActive(false);
        failedImage.SetActive(false);
        gauge.fillAmount = 0.1f;
        timer = 0.0f;
        closeTime = 0.0f;

        gaugeCollider = movingGaugeCollider.GetComponent<BoxCollider2D>();
        colliderPosY = 0.0f;
        isReached = false;
        isClose = false;

        heart.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
        //spacebar.transform.localPosition = new Vector3(spacebar.transform.localPosition.x, -250f, 0f);
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (!isClose)
        {
            if (timer <= limitTime) // 등속 감소 게이지
            {
                SubGauge();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gauge.fillAmount += addGauge;
                SoundManager.Instance.PlaySFX("Medical_input");
            }

            if (isReached && timer >= limitTime)
            { //성공
                SetAllStop.Invoke();
                SoundManager.Instance.PlaySFX("Medical_finish");
                CountManager.Instance.AddClearCount();
                successImage.SetActive(true);
                isClose = true;

                Close();
            }
            if (!isReached && timer >= limitTime || gauge.fillAmount >= 1.0f) // 제한 시간이 다 되거나 게이지가 100%에 도달해도 실패!
            {//실패
                SetAllStop.Invoke();
                failedImage.SetActive(true);
                if (stageManager != null)
                    stageManager.DecreasePlayerHp();
                isClose = true;

                Close();
                timer = 0.0f;
            }
        }
        
        colliderPosY = gaugeCollider.offset.y * 2 * gauge.fillAmount; // 게이지 콜라이더 y좌표 움직임
        movingGaugeCollider.transform.localPosition = new Vector3(0, -colliderPosY, 0); // '-'를 안붙혀주면 콜라이더가 반대로 가는데 이걸 이해할 수가 없드아..
    }
    private void FixedUpdate()
    {
        if (isClose) return;

        if (Input.GetKeyDown(KeyCode.Space))
        { HeartAnim(); }
    }
    void HeartAnim()
    {
        Sequence seq = DOTween.Sequence();
        seq.Append(heart.transform.DOScale(0.5f, 0.08f).SetEase(Ease.OutBounce))
           .Append(heart.transform.DOScale(0.3f, 0.08f).SetEase(Ease.InBounce));
    }

    public void SetIsReached()
    {
        isReached = true;
    }
    public void SetIsUnReached()
    {
        isReached = false;
    }
}
