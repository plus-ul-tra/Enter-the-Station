using DG.Tweening;
using UnityEngine;

/// <summary>
/// �������� ��ȯ �� �� ������ �ؽ�Ʈ Ʈ���Դϴ�.
/// ���̵� �� �Ǹ鼭 ���� �������� ����
/// </summary>
public class T_StageStart : MonoBehaviour
{
    public GameObject startUI;
    public RectTransform textRect;

    private CanvasGroup uiCanvasGroup;
    private CanvasGroup textCanvasGroup;

    public float fadeDuration = 2f;
    public float moveUpDuration = 2f;
    public float moveUpDistance = 50f;

    private Sequence seq;

    private void Start()
    {
        uiCanvasGroup = startUI.GetComponent<CanvasGroup>();
        if(uiCanvasGroup != null)
        {
            uiCanvasGroup.alpha = 1;
        }

        textCanvasGroup = textRect.GetComponent<CanvasGroup>();
        if (textCanvasGroup != null)
        {
            textCanvasGroup.alpha = 0f;
        }

        // 10�� ��� �� ���� ( ��Ʈ�� �ƽ� 8�� )
        Invoke("StartStage", 10f);
    }

    /// <summary>
    /// �������� ��ȯ �� �� ������ �ؽ�Ʈ Ʈ���� �����ϴ� �Լ�
    /// </summary>
    public void StartStage()
    {
        // �������� ���� UI Ȱ��ȭ
        startUI.SetActive(true);

        // �Ʒ��� ��ġ��Ų �� ����
        Vector3 startPos = textRect.anchoredPosition;
        textRect.anchoredPosition = startPos - new Vector3(0, moveUpDistance, 0);

        // Ʈ�� ���
        seq = DOTween.Sequence();
        seq.Join(textCanvasGroup.DOFade(1f, fadeDuration));
        seq.Join(textRect.DOAnchorPos(startPos, moveUpDuration).SetEase(Ease.OutCubic));
        seq.Append(uiCanvasGroup.DOFade(0f, fadeDuration));
        seq.OnComplete(() => startUI.SetActive(false));
    }

    private void OnDisable()
    {
        seq?.Kill();
        seq = null;
    }
}
