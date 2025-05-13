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
            duration: 0.5f,       // ��鸱 �ð� (��)
            strength: new Vector2(10f, 10f), // ��鸲 ����
            vibrato: 10,          // ���� Ƚ��
            randomness: 90,       // ���� ����
            snapping: false,      // ���� ���� �̵� ����
            fadeOut: true         // ��鸲�� ���� �پ�����
        );
    }
}