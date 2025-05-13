using UnityEngine;
using UnityEngine.UIElements;

public class ReachScope : BaseGauge
{
    BoxCollider2D gaugeCollider;
    public GameObject child;
    static public bool isReached;
    bool isOver;
    float colliderPosY;

    void Awake()
    {
        gaugeCollider = child.GetComponent<BoxCollider2D>();
    }
    void Start()
    {   
        colliderPosY = 0.0f;
    }
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !isOver)
        {
            gauge.fillAmount += addGauge;
            
            Debug.Log(isReached);
        }
        else if (isReached && time >= 6.5f)
        {
            successImage.SetActive(true);
            isOver = true;
        }
        else if (!isReached && time >= 6.5f)
        {
            failedImage.SetActive(true);
            isOver = true;
        }

        if (time <= 6.5f) // �ð� ���ٵ��� �ʴ� ��
        {
            SubGauge();
        }
        colliderPosY = gaugeCollider.offset.y * 2 * gauge.fillAmount;
        child.transform.localPosition = new Vector3(0, -colliderPosY, 0); // '-'�� �Ⱥ����ָ� �ݶ��̴��� �ݴ�� ���µ� �̰� ������ ���� �����..
    }
}
