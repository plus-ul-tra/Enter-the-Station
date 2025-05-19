using UnityEngine;
using DG.Tweening;
using UnityEngine.UI; // CanvasGroup ��� �� �ʿ�

public class T_StampEffect : MonoBehaviour
{
    // ���� ������
    public Vector3 startScale = new Vector3(4f, 4f, 4f);          
    public float duration = 0.4f;       // ��ü �ִϸ��̼� �ð�

    private RectTransform target;       // ���� ȿ���� �� UI ������Ʈ

    private void Awake()
    {
        target = this.GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        PlayStamp();
    }

    public void PlayStamp()
    {
        // �ʱ�ȭ
        target.localScale = startScale;

        // ��Ʈ�� ������ �ִϸ��̼�
        Sequence seq = DOTween.Sequence();

        seq.Append(target.DOScale(0.8f, duration * 0.6f).SetEase(Ease.OutQuad));
        seq.Append(target.DOScale(1.0f, duration * 0.4f).SetEase(Ease.OutBack));
    }
}
