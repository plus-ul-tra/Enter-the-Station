using UnityEngine;
using UnityEngine.UI;

public class ClockTimer : MonoBehaviour
{

    [Header("분침 / 시침")]
    public RectTransform minuteHand;    // 분침 (UI)
    public RectTransform hourHand;      // 시침 (UI)

    [Header("FillImage")]
    public Image fillImage;

    [Header("참조")]
    public StageManager stageManager;

    private float totalGameSeconds = 12 * 3600f; // 게임 내 하루가 12시간(12시→12시) 기준

    void Update()
    {
        if (stageManager == null) return;

        // 게임 시간 비율 (0 ~ 1)
        float progress = Mathf.Clamp01(stageManager.stageCurTime / stageManager.stageMaxTime);

        // 전체 게임 시간 중 현재 시간
        float gameTime = totalGameSeconds * progress;

        // 시계각도 계산
        float gameMinutes = gameTime / 60f;
        float gameHours = gameTime / 3600f;

        float minuteAngle = -(gameMinutes % 60f) * 6f;       // 360도 / 60분 = 6도
        float hourAngle = -(gameHours % 12f) * 30f;          // 360도 / 12시간 = 30도

        minuteHand.localEulerAngles = new Vector3(0f, 0f, minuteAngle);
        hourHand.localEulerAngles = new Vector3(0f, 0f, hourAngle);

        // fillAmount = 시침 각도 기준
        // minuteAngle은 0(12시) → -360(다시 12시)이므로 -hourAngle / 360
        fillImage.fillAmount = (-hourAngle % 360f) / 360f;
    }
}
