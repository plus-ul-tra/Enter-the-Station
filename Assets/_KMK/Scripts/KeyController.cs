using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEditor.Rendering;

public class KeyController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public UnityEvent SetSuccess;
    public GameObject light1, light2;
    public GameObject KeyHole;
    public GameObject signGroup;

    public int totalSteps; // ���̾� ���ڰ� �� ĭ���� (��: 0~9�� 10ĭ)
    public float snapSpeed; // �����Ǵ� �ӵ�
    public float sensitivity; // ���콺 ����
    float rawAngle;

    int goalNum;
    int lightNum;
    bool isClear;
    Vector2 dirS;
    

    public AudioClip tickSound; // ���̾� ���� �� ƽƽ �Ҹ�
    public AudioClip unlockSound; // Ǯ���� �� ���� �Ҹ�
    public AudioSource audioSource;

    private RectTransform rectTransform;
    private Vector2 centerPos;
    private float currentAngle;
    private float lastStepAngle = -999f; // ƽƽ ���� �ߺ� ����


    public void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        centerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, KeyHole.transform.position);
        //centerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);
        light1.SetActive(false);
        light2.SetActive(false);
        currentAngle = 0f;
        lightNum = 0;   
        isClear = true;
    }

    void Update()
    {
        if (isClear && lightNum < 2) 
        {
            goalNum = Random.Range(0, totalSteps);
            signGroup.transform.GetChild(goalNum).gameObject.SetActive(true);
            lightNum++;
            isClear = false;
            Debug.Log(goalNum); 
        }
         
        
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        centerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, KeyHole.transform.position);
        //centerPos = RectTransformUtility.WorldToScreenPoint(Camera.main, rectTransform.position);

        dirS = eventData.position - centerPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
            Vector2 dir = eventData.position - centerPos;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            if (dirS.y < 0)
            {
                rawAngle = angle + 90f;
            }
            else if (dirS.y > 0)  // �̹��� ������ �����̶� -90�� ����
            {
                rawAngle = angle - 90f;
            }
            // ���� ��ȭ ��� �� ���� ����
            float angleDelta = Mathf.DeltaAngle(currentAngle, rawAngle);
            currentAngle += angleDelta * sensitivity;
        //rectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
        KeyHole.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

            // ƽ ����
            float stepAngle = 360f / totalSteps;
            int stepIndex = Mathf.RoundToInt((currentAngle + 360f) % 360f / stepAngle);
            float steppedAngle = stepIndex * stepAngle;
            
            Debug.Log("steppedAngle: " + steppedAngle);
            if (Mathf.Abs(steppedAngle - lastStepAngle) > stepAngle * 0.5f) // Mathf.Abs(steppedAngle - lastStepAngle) > stepAngle * 0.5f
            {
                StartCoroutine(SnapToAngle(steppedAngle));
                lastStepAngle = steppedAngle;
            if(Mathf.Abs(steppedAngle) == Mathf.Abs(steppedAngle))
                PlayTickSound();
            }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        // ���� ���� ���
        float stepAngle = 360f / totalSteps;
        float snappedAngle = Mathf.Round((currentAngle + 360f) % 360f / stepAngle) * stepAngle;

        // �ڷ�ƾ���� �ε巴�� ����
        StartCoroutine(SnapToAngle(0f));

        // ��ȣ ���
        int dialNumber = Mathf.RoundToInt(snappedAngle / stepAngle) % totalSteps;
        dialNumber = Mathf.Abs(dialNumber);

        if (dialNumber == goalNum)
        { 
            PlayUnlockSound();  
            SetSuccess.Invoke();
            if (lightNum == 1) { light1.SetActive(true); }
            else if (lightNum == 2) { light2.SetActive(true); }
            signGroup.transform.GetChild(goalNum).gameObject.SetActive(false);
            isClear = true; 
        }
    }

    private System.Collections.IEnumerator SnapToAngle(float targetAngle)
    {
        float startAngle = currentAngle;
        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * snapSpeed;
            currentAngle = Mathf.LerpAngle(startAngle, targetAngle, t);
            //rectTransform.rotation = Quaternion.Euler(0, 0, currentAngle);
            KeyHole.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
            yield return null;
        }

        currentAngle = targetAngle;
        //KeyHole.transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }

    void PlayTickSound()
    {
        if (audioSource && tickSound)
        {
            audioSource.PlayOneShot(tickSound);
        }
    }

    void PlayUnlockSound()
    {
        if (audioSource && unlockSound)
        {
            audioSource.PlayOneShot(unlockSound);
        }
    }
}
