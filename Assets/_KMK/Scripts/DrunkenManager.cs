using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class DrunkenManager : Task
{
    public UnityEvent onSpacebar;
    public UnityEvent onResult;

    public GameObject baseLineGroup;
    public GameObject pressSignGroup;

    Image img;

    bool isReached;
    bool isLeft;
    bool isOver;
    bool isClose;

    public int countLevel;

    //float time;

    void OnEnable()
    {
        InitGame();
    }
    public override void InitGame()
    {
        baseLineGroup.transform.GetChild(0).gameObject.SetActive(true);
        for (int i = 1; i < 5; i++)
        {
            baseLineGroup.transform.GetChild(i).gameObject.SetActive(false);
        }

        successImage.SetActive(false);
        failedImage.SetActive(false);

        isReached = false;
        isOver = false;
        isClose = false;

        countLevel = 0;
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (isReached)  // 스마일 아이콘
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

                if (isLeft) { PopEffect(0); }
                else if (!isLeft) { PopEffect(1); }

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
        if (isOver && timer < limitTime)
        {
            successImage.SetActive(true);
            onResult.Invoke();
            if (!isClose)
            {
                isClose = true;
                Close();
            }
        }

        else if (!isOver && timer >= limitTime)
        {
            if (stageManager != null)
                stageManager.DecreasePlayerHp();
            failedImage.SetActive(true);
            onResult.Invoke();
            if (!isClose)
            {
                isClose = true;
                Close();
            }
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
    public void PopEffect(int i)
    {
        Sequence seq = DOTween.Sequence();
        img = pressSignGroup.transform.GetChild(i).transform.GetChild(countLevel).GetComponent<Image>();

        // 커졌다가 줄어들면서 사라지기
        seq.Append(img.transform.DOScale(1.3f, 0.2f).SetEase(Ease.OutBack))
           .Join(img.DOFade(0f, 0.3f)) // 동시에 투명도 감소
           .Append(img.transform.DOScale(0f, 0.2f).SetEase(Ease.InBack))
           .OnComplete(() => {
               img.gameObject.SetActive(false); // 애니메이션 끝나면 비활성화
               // 재사용을 위한 복원
               img.color = new Color(img.color.r, img.color.g, img.color.b, 1f);
               img.transform.localScale = Vector3.one;
           });
    }
}
