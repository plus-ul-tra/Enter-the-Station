using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
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

    void OnEnable()
    {
        InitGame();
        Initial.Invoke();
    }
    public override void InitGame()
    {
        SuccessImage.SetActive(false);
        FailedImage.SetActive(false);
        time = 0.0f;
        closeTime = 0.0f;
        isClose = false;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !isClose)
        {
            if (isSuccess)
            {
                SuccessImage.SetActive(true);
                isClose = true;
            }

            else if (!isSuccess)
            {
                FailedImage.SetActive(true);
                isClose = true;
            }
        }
        if (time >= 6.5f && !isSuccess)
        {
            FailedImage.SetActive(true);
            isClose = true;
        }

        if (isClose) { SetAllStop.Invoke(); closeTime += Time.deltaTime; }
        if (closeTime >= 1.0f) { Close(); }
    }

    public void SetSuccess() { isSuccess = true; }
    public void SetFail() { isSuccess = false; }
}
