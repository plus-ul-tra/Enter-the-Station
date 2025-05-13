using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseGauge : MonoBehaviour
{
    public Image gauge;
    public GameObject successImage;
    public GameObject failedImage;

    public float addGauge; // �ۿ��� ����
    public float subtractGauge;
    protected float time;
    protected float subTime;

    void Start()
    {
        gauge.fillAmount = 0.0f;
        time = 0;
        successImage.SetActive(false);
        failedImage.SetActive(false);
    }
    protected void SubGauge() // 0.5�ʸ��� subtractGauge�� ��
    {
        subTime += Time.deltaTime;
        if (subTime >= 0.1f) // �������� �� ���� �ʾ�����
        {
            subTime = 0.0f;
            gauge.fillAmount -= subtractGauge;
            //AnimSB(true); // �������� ���� ������ �����̽��ٸ� ������� �ִϸ��̼��� ������
        }
    }
    //void AnimSB(bool a) // �����̽��� �ִϸ��̼�
    //{
    //    anim.SetBool("doWeHave2Press", a);
    //    //anim.SetTrigger("trigger");
    //}
}
