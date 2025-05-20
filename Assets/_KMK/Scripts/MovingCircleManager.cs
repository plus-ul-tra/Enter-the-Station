using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Splines.ExtrusionShapes;


public class MovingCircleManager : Task
{
    public UnityEvent Initial;
    public UnityEvent SetAllStop;

    bool isSuccess;
    bool isClose;

    float time;
    float closeTime;
   
    [SerializeField]
    private MovingCircle circle;
    //private int count;
    void OnEnable()
    {
       // count = 0;
        InitGame();
        Initial.Invoke();
    }
    public override void InitGame()
    {

        successImage.SetActive(false);
        failedImage.SetActive(false);
        timer = 0.0f;
        isClose = false;
        //isSuccess = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !isClose)
        {
            if (isSuccess)
            {
                //3번 성공 종료
                circle.SetSmile();
                successImage.SetActive(true);
                isClose = true;
                Close();
                timer = 0.0f;
            }

            else if (!isSuccess)
            {
                if (stageManager != null)
                    stageManager.DecreasePlayerHp();
                failedImage.SetActive(true);
                isClose = true;
                Close();
            }
        }

        //타임오버
        if (timer >= limitTime && !isSuccess)
        {
            if (stageManager != null)
                stageManager.DecreasePlayerHp();
            failedImage.SetActive(true);
            isClose = true;
            Close();
            timer = 0.0f;
        }

        if (isClose) { SetAllStop.Invoke();}
        //if (closeTime >= 1.0f) { Close(); }
    }

    public void SetSuccess() {  isSuccess = true; }
    public void SetFail() { isSuccess = false; }
}
