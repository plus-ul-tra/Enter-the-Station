using UnityEngine;
using UnityEngine.Events;

public class TurningKeyManager : Task
{
    public UnityEvent Initial;
    public UnityEvent SendIsOver;

    public GameObject firstScene;
    public GameObject secondScene;

    [SerializeField]
    private GameObject lightGroup;

    public int successNum;
    int clearNum;
    bool isClear;
    bool transition;
    bool isDone;

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

        for (int i = 0; i < successNum; i++)
        {
            lightGroup.transform.GetChild(i).GetChild(1).gameObject.SetActive(false);
        } // light on ������Ʈ�� �� ����

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
        {
            lightGroup.transform.GetChild(clearNum).GetChild(1).gameObject.SetActive(true);
            clearNum++; 
            isClear = false; 
            SoundManager.Instance.PlaySFX("Fix_escalate_complete_input"); 
        }

        //if (secondScene.activeSelf == false) return;

        timer += Time.deltaTime;

        if (clearNum == successNum && timer <= limitTime && !isDone)
        {
            isDone = true;
            SendIsOver.Invoke(); // KeyControllers ��ũ��Ʈ�� ���� �ִ� ��ü(TurnKey-KeyAround-KeyHandle)�� SetisOver()
            //Debug.Log("����");
            successImage.SetActive(true);
            CountManager.Instance.AddClearCount();
            SoundManager.Instance.PlaySFX("Fix_escalate_finish");
            Close();
            timer = 0.0f;
        }

        if (clearNum < successNum && timer >= limitTime &&!isDone)
        {
            isDone = true;
            SendIsOver.Invoke();
            //Debug.Log("����");
            SoundManager.Instance.PlaySFX("Fail_sound");
            if(stageManager !=null)
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
