using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public UnityEvent SetSuccess, SetFail;
    public int totalSteps = 10; // 다이얼에 숫자가 몇 칸인지 (예: 0~9면 10칸)
    public float snapSpeed = 10f; // 스냅되는 속도
    int goalNum1, goalNum2, goalNum3;
    public AudioClip tickSound; // 다이얼 돌릴 때 틱틱 소리
    public AudioSource audioSource;

    private RectTransform rectTransform;
    private Vector2 centerPos;
    private float currentAngle = 0f;
    private float lastStepAngle = -999f; // 틱틱 사운드 중복 방지

    private bool isDragging = false;

    public void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        centerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
        goalNum1 = Random.Range(0, totalSteps);
        goalNum2 = Random.Range(0, totalSteps);
        goalNum3 = Random.Range(0, totalSteps);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;
        centerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 dir = eventData.position - centerPos;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        currentAngle = angle - 90f; // 이미지 위쪽이 기준이라 -90도 조정

        // 회전 적용
        rectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);

        // 사운드 틱틱 처리
        float stepAngle = 360f / totalSteps;
        int stepIndex = Mathf.RoundToInt((currentAngle + 360f) % 360f / stepAngle);

        float steppedAngle = stepIndex * stepAngle;
        if (Mathf.Abs(steppedAngle - lastStepAngle) > stepAngle * 0.5f)
        {
            lastStepAngle = steppedAngle;
            PlayTickSound();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        // 스냅 각도 계산
        float stepAngle = 360f / totalSteps;
        float snappedAngle = Mathf.Round((currentAngle + 360f) % 360f / stepAngle) * stepAngle;

        // 코루틴으로 부드럽게 스냅
        StartCoroutine(SnapToAngle(snappedAngle));

        // 번호 계산
        int dialNumber = Mathf.RoundToInt(snappedAngle / stepAngle) % totalSteps;
        Debug.Log("다이얼 번호: " + dialNumber);

        if (dialNumber == goalNum1)
        { }
    }

    private System.Collections.IEnumerator SnapToAngle(float targetAngle)
    {
        float startAngle = currentAngle;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;
            float angle = Mathf.LerpAngle(startAngle, targetAngle, t);
            currentAngle = angle;
            rectTransform.rotation = Quaternion.Euler(0, 0, angle);
            yield return null;
        }

        currentAngle = targetAngle;
        rectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    void PlayTickSound()
    {
        if (audioSource && tickSound)
        {
            audioSource.PlayOneShot(tickSound);
        }
    }
}
