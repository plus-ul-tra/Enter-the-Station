using UnityEngine;
using UnityEngine.UIElements;

public class ReachScope : BaseGauge
{
    BoxCollider2D gaugeCollider;
    public GameObject movingGaugeCollider;
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
        gauge.fillAmount = 0.7f;
        timer = 0.0f;
        closeTime = 0.0f;

        gaugeCollider = movingGaugeCollider.GetComponent<BoxCollider2D>();
        colliderPosY = 0.0f;
        isReached = false;
        isClose = false;
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
                //Debug.Log(isReached);
            }

            if (isReached && timer >= limitTime)
            { //성공
                Debug.Log("성공");
                successImage.SetActive(true);
                isClose = true;
                Close();
            }
            if (!isReached && timer >= limitTime)
            {//실패
                Debug.Log("실패");
                failedImage.SetActive(true);
                stageManager.DecreasePlayerHp();
                isClose = true;
                Close();
                timer = 0.0f;
            }
        }
        
        colliderPosY = gaugeCollider.offset.y * 2 * gauge.fillAmount; // 게이지 콜라이더 y좌표 움직임
        movingGaugeCollider.transform.localPosition = new Vector3(0, -colliderPosY, 0); // '-'를 안붙혀주면 콜라이더가 반대로 가는데 이걸 이해할 수가 없드아..
        if (gauge.fillAmount >= 1.0f) { failedImage.SetActive(true); isClose = true; Close(); timer = 0.0f; return; } // 게이지가 100%에 도달해도 실패!

        //if (isClose) { Close(); }
        //if (closeTime >= 1.0f) { Close(); }
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
