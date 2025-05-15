using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

public class KeyController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public UnityEvent SetSuccess, SetFail;
    public int totalSteps = 10; // ���̾� ���ڰ� �� ĭ���� (��: 0~9�� 10ĭ)
    public float snapSpeed = 10f; // �����Ǵ� �ӵ�
    int goalNum1, goalNum2, goalNum3;
    public AudioClip tickSound; // ���̾� ���� �� ƽƽ �Ҹ�
    public AudioSource audioSource;

    private RectTransform rectTransform;
    private Vector2 centerPos;
    private float currentAngle = 0f;
    private float lastStepAngle = -999f; // ƽƽ ���� �ߺ� ����

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
        currentAngle = angle - 90f; // �̹��� ������ �����̶� -90�� ����

        // ȸ�� ����
        rectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);

        // ���� ƽƽ ó��
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

        // ���� ���� ���
        float stepAngle = 360f / totalSteps;
        float snappedAngle = Mathf.Round((currentAngle + 360f) % 360f / stepAngle) * stepAngle;

        // �ڷ�ƾ���� �ε巴�� ����
        StartCoroutine(SnapToAngle(snappedAngle));

        // ��ȣ ���
        int dialNumber = Mathf.RoundToInt(snappedAngle / stepAngle) % totalSteps;
        Debug.Log("���̾� ��ȣ: " + dialNumber);

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
