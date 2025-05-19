using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

using UnityEditor.Rendering;
using DG.Tweening;

public class KeyController : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{

    public UnityEvent SetSuccess;
    public GameObject light1, light2;
    public GameObject KeyHole;
    public GameObject signGroup;

    public int totalSteps; // 다이얼에 숫자가 몇 칸인지 (예: 0~9면 10칸)
    public float snapSpeed; // 스냅되는 속도
    public float sensitivity; // 마우스 감도
    float rawAngle;

    int goalNum;
    int lightNum;
    bool isClear;
    Vector2 dirS;
    

    public AudioClip tickSound; // 다이얼 돌릴 때 틱틱 소리
    public AudioClip unlockSound; // 풀었을 때 나는 소리
    public AudioSource audioSource;

    private RectTransform rectTransform;
    private Vector2 centerPos;
    private float currentAngle;
    private float lastStepAngle = -999f; // 틱틱 사운드 중복 방지


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
        Canvas canvas = GetComponentInParent<Canvas>();
        Camera cam = null;
        if (canvas.renderMode == RenderMode.ScreenSpaceCamera || canvas.renderMode == RenderMode.WorldSpace)
        {
            cam = canvas.worldCamera;
        }

        centerPos = RectTransformUtility.WorldToScreenPoint(cam, KeyHole.transform.position);

        dirS = (eventData.position - centerPos).normalized;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 currentDir = (eventData.position - centerPos).normalized;

        // 이전 방향과 현재 방향 사이의 상대적인 각도 계산
        float angleDelta = Vector2.SignedAngle(dirS, currentDir);

        currentAngle += angleDelta * sensitivity;
        KeyHole.transform.rotation = Quaternion.Euler(0, 0, currentAngle);

        dirS = currentDir; // 다음 프레임을 위한 기준 갱신

        // 틱 사운드
        float stepAngle = 360f / totalSteps;
        int stepIndex = Mathf.RoundToInt((currentAngle + 360f) % 360f / stepAngle);
        float steppedAngle = stepIndex * stepAngle;
   
        Debug.Log("steppedAngle: " + steppedAngle);
        if (Mathf.Abs(steppedAngle - lastStepAngle) > stepAngle * 0.5f) // Mathf.Abs(steppedAngle - lastStepAngle) > stepAngle * 0.5f
        {
            StartCoroutine(SnapToAngle(steppedAngle));
            lastStepAngle = steppedAngle;
            PlayTickSound();
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {

        // 스냅 각도 계산
        float stepAngle = 360f / totalSteps;
        float snappedAngle = Mathf.Round((currentAngle + 360f) % 360f / stepAngle) * stepAngle;

        // 원래 각도(0)으로 0.5초 동안 돌아감
        KeyHole.transform.DORotate(new Vector3(0, 0, 0), 0.5f).OnComplete(() =>
        {
            currentAngle = 0f;
        });

        // 번호 계산
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
            t += snapSpeed; // t += Time.deltaTime * snapSpeed;
            currentAngle = Mathf.LerpAngle(startAngle, targetAngle, t);
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
