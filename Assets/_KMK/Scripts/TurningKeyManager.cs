using UnityEngine;
using UnityEngine.Events;

public class TurningKeyManager : Task
{
    public UnityEvent Initial;
    public GameObject firstScene;
    public GameObject secondScene;
    int clearNum;
    bool isClear;
    bool transition;
    bool isDone;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        InitGame();
        Initial.Invoke();
    }
    public override void InitGame()
    {
        successImage.SetActive(false);
        failedImage.SetActive(false);
        firstScene.SetActive(true);
        secondScene.SetActive(false);
        isClear = false;
        isDone = false;
        clearNum = 0;
        transition = false;
        timer = 0.0f;
    }
 

    void Update()
    {
        if (transition)
        {
            SoundManager.Instance.PlaySFX("Fix_escalate_insertkey");
            firstScene.SetActive(false);
            secondScene.SetActive(true);
            transition = false;
        }
   
        if (isClear)
        { clearNum++; isClear = false; SoundManager.Instance.PlaySFX("Fix_escalate_complete_input"); }

        //if (secondScene.activeSelf == false) return;

        timer += Time.deltaTime;

        if (clearNum == 2 && timer <= limitTime && !isDone)
        {
            isDone = true;
            //Debug.Log("성공");
            successImage.SetActive(true);
            SoundManager.Instance.PlaySFX("Fix_escalate_finish");
            Close();
            timer = 0.0f;
        }

        if (clearNum < 2 && timer >= limitTime &&!isDone)
        {
            isDone = true;
            //Debug.Log("실패");
            SoundManager.Instance.PlaySFX("Fail_sound");
            stageManager.DecreasePlayerHp();
            failedImage.SetActive(true);
            Close();
            timer = 0.0f;
        }
    }
    public void SetisClear()
    { isClear = true; }

    public void SetTransition()
    { transition = true; }
}
