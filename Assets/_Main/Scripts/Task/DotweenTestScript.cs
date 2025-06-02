using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ImageShaker : MonoBehaviour
{
    public Image targetImage;

    public void ShakeImage()
    {
        if (targetImage == null) return;

        RectTransform rectTransform = targetImage.GetComponent<RectTransform>();

        // DOShakeAnchorPos(duration, strength, vibrato, randomness, snapping, fadeOut)
        rectTransform.DOShakeAnchorPos(
            duration: 0.5f,       // 흔들릴 시간 (초)
            strength: new Vector2(10f, 10f), // 흔들림 강도
            vibrato: 10,          // 진동 횟수
            randomness: 90,       // 랜덤 정도
            snapping: false,      // 정수 단위 이동 여부
            fadeOut: true         // 흔들림이 점점 줄어드는지
        );
    }
}