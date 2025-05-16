using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIAction : MonoBehaviour
{
    private RectTransform rect;
    private void OnEnable()
    {
        ShowAction();
     }

    private void OnDisable()
    {
        // HideAction();
    }

    public void ShowAction()
    {
        if (rect == null) rect = GetComponent<RectTransform>();


        Vector2 targetPos = Vector2.zero;
        Vector2 overshootPos = new Vector2(0, -80); // ��ǥ���� �Ʒ�

        // �ϴ� �Ʒ��� �������ٰ� �ö����
        rect.anchoredPosition = new Vector2(0, 1400); // ���� ��ġ ��
        rect.DOAnchorPos(overshootPos, 0.5f)
            .SetEase(Ease.OutCubic)
            .OnComplete(() =>
            {
                rect.DOAnchorPos(targetPos, 0.3f).SetEase(Ease.InOutCubic);
            });
    }
    public void HideAction(GameObject child)
    {
        Debug.Log("HideAction");
        Vector2 currentPos = rect.anchoredPosition;
        Vector2 targetPos = currentPos + new Vector2(0, 1500); // ���� �̵�

        rect.DOAnchorPos(targetPos, 0.8f)
            .SetEase(Ease.InCubic)
        .OnComplete(() =>
        {
            Debug.Log("�ִ� �Ϸ�");
            child.SetActive(false); // �ִϸ��̼� �Ϸ� �� ��
            gameObject.SetActive(false); // �ڱ� �ڽŵ� ��
        });
    }
}
