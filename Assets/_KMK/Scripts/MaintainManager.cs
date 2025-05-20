using UnityEngine;
using DG.Tweening;
using UnityEngine.UIElements;

public class MaintainManager : BaseGauge
{
    BoxCollider2D gaugeCollider;
    public GameObject movingGaugeCollider;

    [SerializeField]
    private GameObject spacebar;
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
        spacebar.transform.localPosition = new Vector3(spacebar.transform.localPosition.x, -250f, 0f);
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
                SoundManager.Instance.PlaySFX("Medical_input");
            }

            if (isReached && timer >= limitTime)
            { //����
                //Debug.Log("����");
                SoundManager.Instance.PlaySFX("Medical_finish");
                successImage.SetActive(true);
                isClose = true;
                Close();
            }
            if (!isReached && timer >= limitTime)
            {//����
                //Debug.Log("����");
                failedImage.SetActive(true);
                stageManager.DecreasePlayerHp();
                isClose = true;
                Close();
                timer = 0.0f;
            }
        }
        
        colliderPosY = gaugeCollider.offset.y * 2 * gauge.fillAmount; // ������ �ݶ��̴� y��ǥ ������
        movingGaugeCollider.transform.localPosition = new Vector3(0, -colliderPosY, 0); // '-'�� �Ⱥ����ָ� �ݶ��̴��� �ݴ�� ���µ� �̰� ������ ���� �����..
        if (gauge.fillAmount >= 1.0f) { failedImage.SetActive(true); isClose = true; Close(); timer = 0.0f; return; } // �������� 100%�� �����ص� ����!
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
