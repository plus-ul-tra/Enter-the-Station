using UnityEngine;
using UnityEngine.UI;
public class TimeFrame : MonoBehaviour
{
    private Task task;
    [SerializeField]
    private Image timeFrame;
    private float timer;
    private void OnEnable()
   {
        timer = 0.0f;
        task = GetComponentInParent<Task>();
    }

    private void Update()
    {
        
        if (task == null || timeFrame == null) return;
        timer += Time.deltaTime;
        float progress = Mathf.Clamp01(1.0f - (timer / task.GetLimitTime()));
        timeFrame.fillAmount = progress;
        float t = Mathf.Clamp01(timer / task.GetLimitTime());
        float fill = Mathf.Lerp(1.0f, 0.276f, t);
        timeFrame.fillAmount = fill;
    }
}
