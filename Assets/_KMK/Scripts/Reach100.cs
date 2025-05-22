using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

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

    public UnityEvent SetAllStop;
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
            
            if (timer <= limitTime && !isFilled) // �ð� ���ٵ��� �ʴ� ��
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
                SetAllStop.Invoke(); // ���� �ٸ� ������Ʈ�� anim�� �����. Spacebar ������Ʈ�� SetisOver() �Լ� �ֱ�
                SoundManager.Instance.PlaySFX("Medical_finish");
                Close();
                timer = 0.0f;
            }
            else if (!isFilled && timer >= limitTime)
            {
                //Debug.Log("ȣȣȣǮ�kǪ");
                if (stageManager != null)
                    stageManager.DecreasePlayerHp();
                failedImage.SetActive(true);
                SoundManager.Instance.PlaySFX("Fail_sound");
                isClose = true;
                SetAllStop.Invoke(); // ���� �ٸ� ������Ʈ�� anim�� �����. Spacebar ������Ʈ�� SetisOver() �Լ� �ֱ�
                Close();
                timer = 0.0f;
            }
        }
        
       
    }
}
