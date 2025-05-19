using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;
using UnityEditor.Rendering;
using TreeEditor;
using UnityEngine.Rendering.Universal;

public class MovingKey : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public UnityEvent transition;
    private RectTransform rectTransform;
    public float speed;
    float delay;
    Vector2 pointerOffset; // 오프셋 저장용
    bool isMoving;

    public void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(480f, rectTransform.localPosition.y, rectTransform.localPosition.z);
        isMoving = true;
        delay = 0f;
    }

    void Update()
    {
        if (isMoving) return;
        delay += Time.deltaTime;
        if (rectTransform.localPosition.x >= -14f)
        {
            rectTransform.Translate(-1 * speed, 0, 0);
        }
        if (rectTransform.localPosition.x <= -14f && delay > 0.6f)
        {
            transition.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Canvas canvas = GetComponentInParent<Canvas>();
        Camera cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            cam = canvas.worldCamera;
        }
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        rectTransform,
        eventData.position,
        cam, // null or eventData.pressEventCamera,
        out pointerOffset
    );
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isMoving) return;
        Canvas canvas = GetComponentInParent<Canvas>();
        Camera cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            cam = canvas.worldCamera;
        }
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
        rectTransform.parent as RectTransform,
        eventData.position,
        cam, // null or eventData.pressEventCamera,
        out Vector2 localPoint))
        {
            // 오프셋을 보정하여 자연스럽게 따라오도록
            Vector3 newPos = new Vector3(localPoint.x - pointerOffset.x, rectTransform.localPosition.y, rectTransform.localPosition.z);

            // x 범위 제한
            newPos.x = Mathf.Min(newPos.x, 505f);

            rectTransform.localPosition = newPos;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isMoving = false;
    }
}
