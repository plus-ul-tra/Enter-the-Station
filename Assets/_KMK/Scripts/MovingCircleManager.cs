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
    
    public GameObject SuccessImage;
    public GameObject FailedImage;
    [SerializeField]
    private MovingCircle circle;
    void OnEnable()
    {
        InitGame();
        Initial.Invoke();
    }
    public override void InitGame()
    {
        SuccessImage.SetActive(false);
        FailedImage.SetActive(false);
        timer = 0.0f;
        isClose = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !isClose)
        {
            if (isSuccess)
            {
                circle.SetSmile();
                SuccessImage.SetActive(true);
                isClose = true;
                Close();
            }

            else if (!isSuccess)
            {
                FailedImage.SetActive(true);
                isClose = true;
                Close();
            }
        }
        if (timer >= limitTime && !isSuccess)
        {
            FailedImage.SetActive(true);
            isClose = true;
            Close();
        }

        if (isClose) { SetAllStop.Invoke();}
        //if (closeTime >= 1.0f) { Close(); }
    }

    public void SetSuccess() { isSuccess = true; }
    public void SetFail() { isSuccess = false; }
}
