using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Reach100 : BaseGauge
{
    bool isFilled;
    void Start()
    {
        gauge.fillAmount = 0.0f;
        time = 0;
        successImage.SetActive(false);
        failedImage.SetActive(false);
    }
    void Update()
    {    
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !isFilled)
        {
            gauge.fillAmount += addGauge;
            if (gauge.fillAmount == 1.0f)
            {
                isFilled = true;         
            }
            Debug.Log(isFilled);
            Debug.Log(time);
        }
        else if (isFilled && time <= 6.5f)
        {
            successImage.SetActive(true);
        }
        else if (!isFilled && time >= 6.5f)
        {
            failedImage.SetActive(true);
        }

        if (time <= 6.5f && !isFilled) // �ð� ���ٵ��� �ʴ� ��
        {
            SubGauge();
        }
    }
}
