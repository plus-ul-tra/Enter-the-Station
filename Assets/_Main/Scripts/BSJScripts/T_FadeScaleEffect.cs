using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class T_FadeScaleEffect : MonoBehaviour
{
    public Image targetImage;       // ��� �̹���
    public float duration = 1.0f;   // �ִϸ��̼� �ð�
    public float targetScale = 2f;  // ���� ������ ���

    private void Start()
    {
        PlayEffect();
    }

    public void PlayEffect()
    {
        // �̹��� �ʱ� ���� ����
        targetImage.color = new Color(1, 1, 1, 1); // ���� ������
        targetImage.transform.localScale = Vector3.one;

        // ���ÿ� �����Ͼ� + ���̵�ƿ�
        Sequence seq = DOTween.Sequence();

        seq.Join(targetImage.transform.DOScale(targetScale, duration).SetEase(Ease.OutQuad));
        seq.Join(targetImage.DOFade(0f, duration).SetEase(Ease.InQuad));

        // ���� �ڿ� ��Ȱ��ȭ�ϰ� �ʹٸ�
        seq.OnComplete(() => Destroy(this.gameObject));
    }
}
