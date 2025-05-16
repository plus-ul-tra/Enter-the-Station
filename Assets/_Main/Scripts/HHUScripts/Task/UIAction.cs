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
        Vector2 overshootPos = new Vector2(0, -80); // 목표보다 아래

        // 일단 아래로 내려갔다가 올라오기
        rect.anchoredPosition = new Vector2(0, 1400); // 시작 위치 위
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
        Vector2 targetPos = currentPos + new Vector2(0, 1500); // 위로 이동

        rect.DOAnchorPos(targetPos, 0.8f)
            .SetEase(Ease.InCubic)
        .OnComplete(() =>
        {
            Debug.Log("애니 완료");
            child.SetActive(false); // 애니메이션 완료 후 끔
            gameObject.SetActive(false); // 자기 자신도 끔
        });
    }
}
