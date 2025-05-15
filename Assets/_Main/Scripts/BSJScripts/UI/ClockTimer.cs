using UnityEngine;

public class ClockTimer : MonoBehaviour
{
    [Header("��ħ / ��ħ")]
    public RectTransform minuteHand;    // ��ħ (UI)
    public RectTransform hourHand;      // ��ħ (UI)

    [Header("����")]
    public StageManager stageManager;

    private float totalGameSeconds = 9f * 3600f; // 9�ð� = 32,400��

    void Update()
    {
        if (stageManager == null) return;

        // ���� ����� ���� �ð� (���� ���� ���� �ð� ���)
        float progress = Mathf.Clamp01(stageManager.stageCurTime / stageManager.stageMaxTime);
        float gameTime = totalGameSeconds * progress;

        float gameMinutes = gameTime / 60f;
        float gameHours = gameTime / 3600f;

        float minuteAngle = -(gameMinutes % 60f) * 6f;
        float hourAngle = -(9f + (gameHours % 9f)) * 30f; // 9�� ���� ����

        minuteHand.localEulerAngles = new Vector3(0f, 0f, minuteAngle);
        hourHand.localEulerAngles = new Vector3(0f, 0f, hourAngle);
    }
}
