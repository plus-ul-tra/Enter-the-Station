using UnityEngine;
using UnityEngine.UI;

public class GaugeFollower : MonoBehaviour
{
    public Image gaugeFillImage;       // �Ķ� ������ (Filled)
    public RectTransform icon;         // ���� ��
    public RectTransform fillArea;     // �Ķ� ������ �̹����� RectTransform

    void Update()
    {
        float fillAmount = gaugeFillImage.fillAmount;

        float fillHeight = fillArea.rect.height;
        float targetY = fillAmount * fillHeight;

        // localPosition ����, pivot.y = 0 �̸� �Ʒ� ����
        icon.localPosition = new Vector3(icon.localPosition.x, targetY-25, icon.localPosition.z);
    }
}

