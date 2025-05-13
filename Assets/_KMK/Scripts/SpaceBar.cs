using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBar : MonoBehaviour
{
    public Image gauge;
    public GameObject successImage;
    public GameObject failedImage;
    Animator anim;

    public float addGauge;
    public float subtractGauge;
    float time;
    float subTime;
    bool IsFilled;

    void Start()
    {
        gauge.fillAmount = 0.0f;
        time = 0;
        IsFilled = false;
        successImage.SetActive(false);
        failedImage.SetActive(false);
        
        anim = GetComponent<Animator>();
    }

    void Update()
    {    
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !IsFilled)
        {
            gauge.fillAmount += addGauge;
            if (gauge.fillAmount == 1.0f)
            {
                IsFilled = true;         
            }
            Debug.Log(IsFilled);
            Debug.Log(time);
        }
        else if (IsFilled && time <= 6.5f)
        {
            successImage.SetActive(true);
        }
        else if (!IsFilled && time >= 6.5f)
        {
            failedImage.SetActive(true);
        }

        if (time <= 6.5f) // �ð� ���ٵ��� �ʴ� ��
        {
            SubGauge();
        }
    }

    void SubGauge() // 0.5�ʸ��� subtractGauge�� ��
    {
        subTime += Time.deltaTime;
        if (subTime >= 0.5f && !IsFilled) // �������� �� ���� �ʾ�����
        { 
            subTime = 0.0f;
            gauge.fillAmount -= subtractGauge;
            AnimSB(true); // �������� ���� ������ �����̽��ٸ� ������� �ִϸ��̼��� ������
        }
    }
    void AnimSB(bool a) // �����̽��� �ִϸ��̼�
    {
        anim.SetBool("doWeHave2Press" , a);
        //anim.SetTrigger("trigger");
    }
}
