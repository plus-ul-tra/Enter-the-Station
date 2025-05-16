using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Reach100 : BaseGauge
{
    bool isFilled;

    void OnEnable()
    {
        Debug.Log("chchchc");
        InitGame();
    }
    public override void InitGame()
    {
        successImage.SetActive(false);
        failedImage.SetActive(false);
        gauge.fillAmount = 0.0f;
        time = 0.0f;
        closeTime = 0.0f;
        isClose = false;
        isFilled = false;
    }
    void Update()
    {    
        time += Time.deltaTime;
        if (!isClose)
        {
            if (time <= 6.5f && !isFilled) // 시간 오바되지 않는 한
            {
                SubGauge();
            }

            if (Input.GetKeyDown(KeyCode.Space) && !isFilled)
            {
                gauge.fillAmount += addGauge;
                if (gauge.fillAmount == 1.0f)
                {
                    isFilled = true;
                }
                //Debug.Log(isFilled);
                //Debug.Log(time);
            }
            else if (isFilled && time <= 6.5f)
            {
                //successImage.SetActive(true);
                isClose = true;
                Close();
            }
            else if (!isFilled && time >= 6.5f)
            {
                //failedImage.SetActive(true);
                isClose = true;
                Close();
            }
        }
        
       
    }
}
