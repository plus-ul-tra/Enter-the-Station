using UnityEngine;
using UnityEngine.UI;

public class GaugeFollower : MonoBehaviour
{
    public Image gaugeFillImage;       // 파란 게이지 (Filled)
    public RectTransform icon;         // 따라갈 원
    public RectTransform fillArea;     // 파란 게이지 이미지의 RectTransform

    void Update()
    {
        float fillAmount = gaugeFillImage.fillAmount;

        float fillHeight = fillArea.rect.height;
        float targetY = fillAmount * fillHeight;

        // localPosition 기준, pivot.y = 0 이면 아래 기준
        icon.localPosition = new Vector3(icon.localPosition.x, targetY-25, icon.localPosition.z);
    }
}

