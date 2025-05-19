using UnityEngine;
using UnityEngine.Events;

public class ReachAim : Task
{
    public UnityEvent onSpacebar;
    public UnityEvent onResult;
    public UnityEvent initial;

    public GameObject successImage;
    public GameObject failedImage;
    public GameObject baseLineGroup;
    public GameObject pressSignGroup;

    bool isReached;
    bool isLeft;
    bool isOver;
    bool isClose;

    public int countLevel;

    float time;

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
        isReached = false;
        isOver = false;
        isClose = false;

        countLevel = 0;
        SetActive();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isReached)
        {
            if (isLeft)
            { pressSignGroup.transform.GetChild(0).transform.GetChild(countLevel).gameObject.SetActive(true); }
            else if (!isLeft)
            { pressSignGroup.transform.GetChild(1).transform.GetChild(countLevel).gameObject.SetActive(true); }
        }
        else if (!isReached)
        {
            if (isLeft)
            { pressSignGroup.transform.GetChild(0).transform.GetChild(countLevel).gameObject.SetActive(false); }
            else if (!isLeft)
            { pressSignGroup.transform.GetChild(1).transform.GetChild(countLevel).gameObject.SetActive(false); }
        }

        if (Input.GetKeyDown(KeyCode.Space) && !isOver)
        {    
            if (isReached)
            {
                if (countLevel == 4) { isOver = true;  return; }

                onSpacebar.Invoke();
                baseLineGroup.transform.GetChild(countLevel).gameObject.SetActive(false);

                if (isLeft)
                { pressSignGroup.transform.GetChild(0).transform.GetChild(countLevel).gameObject.SetActive(false); }
                else if (!isLeft)
                { pressSignGroup.transform.GetChild(1).transform.GetChild(countLevel).gameObject.SetActive(false); }

                countLevel++;
                baseLineGroup.transform.GetChild(countLevel).gameObject.SetActive(true);
            }
            else if (!isReached)
            {
                if (countLevel == 0) return;
                baseLineGroup.transform.GetChild(countLevel).gameObject.SetActive(false);
                countLevel--;
                baseLineGroup.transform.GetChild(countLevel).gameObject.SetActive(true);
            }
        }
        if (isOver && time < 6.5f)
        {
            successImage.SetActive(true);
            onResult.Invoke();
            if (!isClose)
            {
                isClose = true;
                Close();
            }
        }

        else if (!isOver && timer >= limitTime || gauge.fillAmount == 0.0f)
        {
            failedImage.SetActive(true);
            onResult.Invoke();
            if (!isClose)
            {
                isClose = true;
                Close();
            }


        }


        if (timer <= limitTime && !isOver) // 시간 오바되지 않는 한 계속 게이지 감소
        {
            SubGauge();      
        }
    }
    public void SetIsReached()
    {
        isReached = true;
    }
    public void SetIsUnReached()
    {
        isReached = false;
    }
    public void SetisLeft() { isLeft = true; }
    public void SetisRight() { isLeft = false; }
    
    void SetActive()
    {
        baseLineGroup.transform.GetChild(0).gameObject.SetActive(true);
        baseLineGroup.transform.GetChild(1).gameObject.SetActive(false);
        baseLineGroup.transform.GetChild(2).gameObject.SetActive(false);
        baseLineGroup.transform.GetChild(3).gameObject.SetActive(false);
        baseLineGroup.transform.GetChild(4).gameObject.SetActive(false);

        pressSignGroup.transform.GetChild(0).gameObject.SetActive(true);  
        pressSignGroup.transform.GetChild(0).transform.GetChild(0).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(0).transform.GetChild(1).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(0).transform.GetChild(2).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(0).transform.GetChild(3).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(0).transform.GetChild(4).gameObject.SetActive(false);

        pressSignGroup.transform.GetChild(1).gameObject.SetActive(true);
        pressSignGroup.transform.GetChild(1).transform.GetChild(0).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(1).transform.GetChild(1).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(1).transform.GetChild(2).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(1).transform.GetChild(3).gameObject.SetActive(false);
        pressSignGroup.transform.GetChild(1).transform.GetChild(4).gameObject.SetActive(false);
    }
}
