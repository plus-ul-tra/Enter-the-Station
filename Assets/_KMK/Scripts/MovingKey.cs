using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class MovingKey : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    public UnityEvent transition;
    private RectTransform rectTransform;
    public float speed;
    float time;
    Vector2 mousPos;
    Vector2 pointerOffset; // 오프셋 저장용
    bool isMoving;

    public void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localPosition = new Vector3(480f, rectTransform.localPosition.y, rectTransform.localPosition.z);
        isMoving = true;
        time = 0f;
    }

    void Update()
    {
        float snapSpeed = Time.deltaTime * speed;
        if (isMoving) return;
        time += Time.deltaTime;
        
        if (time > 0.001f && rectTransform.localPosition.x >= -16.5f)
        {
            rectTransform.Translate(-1 * snapSpeed, 0, 0);
            time = 0f;
        }
        if (rectTransform.localPosition.x <= -16.5f && time > 1f)
        {
            transition.Invoke();
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
        rectTransform,
        eventData.position,
        eventData.pressEventCamera,
        out pointerOffset
    );
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!isMoving) return;

        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
        rectTransform.parent as RectTransform,
        eventData.position,
        eventData.pressEventCamera,
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
//if (dir.x > 0)
//{
//    //rectTransform.position = new Vector3(eventData.position.x, 28, 0);
//    //Debug.Log(dir.x);
//}
//else if (dir.x < 0)
//{
//    //rectTransform.position = new Vector3(eventData.position.x, 28, 0);
//    //Debug.Log(dir.x); 
//}