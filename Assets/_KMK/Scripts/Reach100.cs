using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Reach100 : BaseGauge
{
    bool isFilled;

    void OnEnable()
    {
        //Debug.Log("chchchc");
        InitGame();
    }
    public override void InitGame()
    {
        timer = 0.0f;
        successImage.SetActive(false);
        failedImage.SetActive(false);
        gauge.fillAmount = 0.0f;
        isClose = false;
        isFilled = false;
    }
    void Update()
    {    
        timer += Time.deltaTime;
        if (!isClose)
        {
            if (timer <= limitTime && !isFilled) // 시간 오바되지 않는 한
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
            else if (isFilled && timer <= limitTime)
            {
                successImage.SetActive(true);
                isClose = true;
                Close();
            }
            else if (!isFilled && timer >= limitTime)
            {
                failedImage.SetActive(true);
                isClose = true;
                Close();
            }
        }
        
       
    }
}
