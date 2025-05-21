using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Reach100 : BaseGauge
{
    bool isFilled;
    [SerializeField]
    private Sprite[] playerSprites;
    [SerializeField]
    private Sprite[] extraSprites;
    [SerializeField]
    private Image player;
    [SerializeField]
    private Image extra;
    private int index=0;
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
        gauge.fillAmount = 0.4f;
        isClose = false;
        isFilled = false;
    }
    private void ChangeSprite()
    {
        if(index == 0)
        {
            index = 1;
            player.sprite = playerSprites[index];
            extra.sprite = extraSprites[index];
        }
        else if(index == 1)
        {
            index = 0;
            player.sprite = playerSprites[index];
            extra.sprite = extraSprites[index];
        }
            
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
                ChangeSprite();
                gauge.fillAmount += addGauge;
                if (gauge.fillAmount == 1.0f)
                {
                    isFilled = true;
                }
                
            }
            else if (isFilled && timer <= limitTime)
            {
                successImage.SetActive(true);
                CountManager.Instance.AddClearCount();
                isClose = true;
                SoundManager.Instance.PlaySFX("Medical_finish");
                Close();
                timer = 0.0f;
            }
            else if (!isFilled && timer >= limitTime)
            {
                //Debug.Log("호호호풀훛푸");
                if (stageManager != null)
                    stageManager.DecreasePlayerHp();
                failedImage.SetActive(true);
                SoundManager.Instance.PlaySFX("Fail_sound");
                isClose = true;
                Close();
                timer = 0.0f;
            }
        }
        
       
    }
}
