using UnityEngine;
using UnityEngine.UI;

public class ClockTimer : MonoBehaviour
{

    [Header("��ħ / ��ħ")]
    public RectTransform minuteHand;    // ��ħ (UI)
    public RectTransform hourHand;      // ��ħ (UI)

    [Header("FillImage")]
    public Image fillImage;

    [Header("����")]
    public StageManager stageManager;

    private float totalGameSeconds = 12 * 3600f; // ���� �� �Ϸ簡 12�ð�(12�á�12��) ����

    void Update()
    {
        if (stageManager == null) return;

        // ���� �ð� ���� (0 ~ 1)
        float progress = Mathf.Clamp01(stageManager.stageCurTime / stageManager.stageMaxTime);

        // ��ü ���� �ð� �� ���� �ð�
        float gameTime = totalGameSeconds * progress;

        // �ð谢�� ���
        float gameMinutes = gameTime / 60f;
        float gameHours = gameTime / 3600f;

        float minuteAngle = -(gameMinutes % 60f) * 6f;       // 360�� / 60�� = 6��
        float hourAngle = -(gameHours % 12f) * 30f;          // 360�� / 12�ð� = 30��

        minuteHand.localEulerAngles = new Vector3(0f, 0f, minuteAngle);
        hourHand.localEulerAngles = new Vector3(0f, 0f, hourAngle);

        // fillAmount = ��ħ ���� ����
        // minuteAngle�� 0(12��) �� -360(�ٽ� 12��)�̹Ƿ� -hourAngle / 360
        fillImage.fillAmount = (-hourAngle % 360f) / 360f;
    }
}
