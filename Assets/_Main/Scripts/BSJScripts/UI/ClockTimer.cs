using UnityEngine;

public class ClockTimer : MonoBehaviour
{
    [Header("분침 / 시침")]
    public RectTransform minuteHand;    // 분침 (UI)
    public RectTransform hourHand;      // 시침 (UI)

    [Header("참조")]
    public StageManager stageManager;

    private float totalGameSeconds = 9f * 3600f; // 9시간 = 32,400초

    void Update()
    {
        if (stageManager == null) return;

        // 현재 진행된 게임 시간 (현실 기준 누적 시간 비례)
        float progress = Mathf.Clamp01(stageManager.stageCurTime / stageManager.stageMaxTime);
        float gameTime = totalGameSeconds * progress;

        float gameMinutes = gameTime / 60f;
        float gameHours = gameTime / 3600f;

        float minuteAngle = -(gameMinutes % 60f) * 6f;
        float hourAngle = -(9f + (gameHours % 9f)) * 30f; // 9시 시작 기준

        minuteHand.localEulerAngles = new Vector3(0f, 0f, minuteAngle);
        hourHand.localEulerAngles = new Vector3(0f, 0f, hourAngle);
    }
}
