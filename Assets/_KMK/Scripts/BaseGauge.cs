using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseGauge : Task
{
    public Image gauge;
    public GameObject successImage;
    public GameObject failedImage;

    public float addGauge; // �ۿ��� ����
    public float subtractGauge;
    protected float time;
    protected float subTime;
    protected float closeTime;

    protected bool isClose;

    protected void SubGauge() // 0.1�ʸ��� subtractGauge�� ��
    {
        subTime += Time.deltaTime;
        if (subTime >= 0.1f)
        {
            subTime = 0.0f;
            gauge.fillAmount -= subtractGauge;
            //AnimSB(true); // �������� ���� ������ �����̽��ٸ� ������� �ִϸ��̼��� ������
        }
    }
    protected void SubGauge(float gaugeNum) // 0.1�ʸ��� subtractGauge�� ��
    {
            gauge.fillAmount -= gaugeNum;
    }

    // �����̽��� �ִϸ��̼�
}
