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
            if (timer <= limitTime) // ��� ���� ������
            {
                SubGauge();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gauge.fillAmount += addGauge;
                //Debug.Log(isReached);
            }
            else if (isReached && time >= 4.5f)
            {
                successImage.SetActive(true);
                isClose = true;
                Close();
            }
            else if (!isReached && time >= 4.5f)
            {
                failedImage.SetActive(true);
                isClose = true;
                Close();
            }
        }
        colliderPosY = gaugeCollider.offset.y * 2 * gauge.fillAmount; // ������ �ݶ��̴� y��ǥ ������
        movingGaugeCollider.transform.localPosition = new Vector3(0, -colliderPosY, 0); // '-'�� �Ⱥ����ָ� �ݶ��̴��� �ݴ�� ���µ� �̰� ������ ���� �����..
        if (gauge.fillAmount >= 1.0f && !isClose) { failedImage.SetActive(true); isClose = true; Close(); return; } // �������� 100%�� �����ص� ����!

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
