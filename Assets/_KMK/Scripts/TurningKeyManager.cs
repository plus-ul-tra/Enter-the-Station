using UnityEngine;
using UnityEngine.Events;

public class TurningKeyManager : Task
{
    public UnityEvent Initial;
    public GameObject successImage;
    public GameObject failedImage;
    public GameObject firstScene;
    public GameObject secondScene;
    float time;
    int clearNum;
    bool isClear;
    bool transition;
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
        time = 0.0f;
        isClear = false;
        clearNum = 0;
        transition = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (transition)
        {
            firstScene.SetActive(false);
            secondScene.SetActive(true);
            transition = false;
        }
   
        if (isClear)
        { clearNum++; isClear = false; }

        if (secondScene.activeSelf == false) return;
        time += Time.deltaTime;
        if (clearNum == 2 && time <= 6.5f)
        { successImage.SetActive(true);
            Close();
        }
        //else if (clearNum < 2 && time >= 6.5f)
        //{ failedImage.SetActive(true);
        //    Close();
        //}
    }
    public void SetisClear()
    { isClear = true; }

    public void SetTransition()
    { transition = true; }
}
