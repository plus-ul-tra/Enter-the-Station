using UnityEngine;
using UnityEngine.Events;

public class ReachAim : BaseGauge
{
    public UnityEvent onSpacebar;
    public UnityEvent onResult;
    public UnityEvent initial;

    bool isReached;
    bool isOver;
    
    public float penaltyGauge;

    void OnEnable()
    {
        InitGame();
        initial.Invoke();
    }
    public override void InitGame()
    {
        successImage.SetActive(false);
        failedImage.SetActive(false);
        gauge.fillAmount = 0.5f;
        time = 0.0f;
        closeTime = 0.0f;
        isReached = false;
        isOver = false;
        isClose = false;
    }
    void Update()
    {
        time += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && !isOver)
        {    
            if (isReached)
            {
                gauge.fillAmount += addGauge;
                if (gauge.fillAmount == 1.0f)
                {
                    isOver = true;
                    
                }
            }
            else if (!isReached)
            {
                SubGauge(penaltyGauge);
            }

            onSpacebar.Invoke();
        }
        else if (isOver)
        {
            Close();
            successImage.SetActive(true);
            onResult.Invoke();

            if (!isClose)
            {
                isClose = true;
                Close();
            }
        }
        else if (!isOver && time >= 6.5f || gauge.fillAmount == 0.0f)
        {
            Close();
            failedImage.SetActive(true);
            onResult.Invoke();

            if(!isClose)
            {
                isClose = true;
                Close();
            }

        }


        if (time <= 6.5f && !isOver) // 시간 오바되지 않는 한 계속 게이지 감소
        {
            SubGauge();      
        }
    }
    public void SetIsReached()
    {
        isReached = true;
        Debug.Log(isReached);
    }
    public void SetIsUnReached()
    {
        isReached = false;
        Debug.Log(isReached);
    }
}
