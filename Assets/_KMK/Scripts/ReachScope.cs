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
        gauge.fillAmount = 0.0f;
        time = 0.0f;
        closeTime = 0.0f;

        gaugeCollider = movingGaugeCollider.GetComponent<BoxCollider2D>();
        colliderPosY = 0.0f;
        isReached = false;
        isClose = false;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (!isClose)
        {
            if (time <= 6.5f) // ��� ���� ������
            {
                SubGauge();
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                gauge.fillAmount += addGauge;
                Debug.Log(isReached);
            }
            else if (isReached && time >= 6.5f)
            {
                successImage.SetActive(true);
                isClose = true;
            }
            else if (!isReached && time >= 6.5f)
            {
                failedImage.SetActive(true);
                isClose = true;
            }
        }
        colliderPosY = gaugeCollider.offset.y * 2 * gauge.fillAmount; // ������ �ݶ��̴� y��ǥ ������
        movingGaugeCollider.transform.localPosition = new Vector3(0, -colliderPosY, 0); // '-'�� �Ⱥ����ָ� �ݶ��̴��� �ݴ�� ���µ� �̰� ������ ���� �����..
        if (gauge.fillAmount >= 1.0f) { failedImage.SetActive(true); isClose = true; } // �������� 100%�� �����ص� ����!

        if (isClose) { closeTime += Time.deltaTime; }
        if (closeTime >= 1.0f) { Close(); }
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
