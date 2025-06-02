using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroMoveCamera : MonoBehaviour
{
    [Header("이동 구간")]
    public float startX = -7f;     // 시작 X 좌표
    public float endX = 6f;     // 종료 X 좌표

    [Header("이동 시간 설정")]
    public float moveDuration = 2f;

    [Header("Y축 순간이동 위치")]
    public float snapY = -14f;

    [Header("페이드 지속 시간")]
    [Tooltip("FadeTeleport에 넘길 페이드인/아웃 시간")]
    public float fadeDuration = 1f;

    //[Header("컷씬이면 체크")]
    //[SerializeField] private bool isCutScene;

    [Header("페이드 이미지")]
    public Image fadeImage;

    void Start()
    {
        // 시작 위치 세팅
        var pos = transform.position;
        pos.x = startX;
        transform.position = pos;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        // 1) 첫 번째 X축 이동
        yield return StartCoroutine(MoveX(startX, endX, true));

        // 2) 화면을 완전 검은색으로 채워서
        //var img = ScreenFader.Instance.GetComponent<Image>();
        //img.color = new Color(0, 0, 0, 1);
        //yield return null;

        //if (!isCutScene)
        //{
        //    StartCoroutine(ScreenFader.Instance.Fade(2f, 0f, 2f));
        //}

        // 3) 순간이동 (Y만)
        var p = transform.position;
        p.x = startX;
        p.y = snapY;
        transform.position = p;

        // yield return new WaitForSeconds(1f);

        // 4) 두 번째 X축 이동 (검은 화면 상태에서 실행)
        yield return StartCoroutine(MoveX(startX, endX, false));

    }


    IEnumerator MoveX(float fromX, float toX, bool isOut)
    {
        float speed = 4f; // 초당 이동 속도 (원하는 값으로 조정)

        Vector3 startPos = transform.position;
        startPos.x = fromX;
        Vector3 endPos = startPos;
        endPos.x = toX;

        float totalDistance = Mathf.Abs(toX - fromX);
        float duration = totalDistance / speed;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float t = elapsed / duration;
            transform.position = Vector3.Lerp(startPos, endPos, t);

            Color color = fadeImage.color;

            if (isOut)
            {
                if (t >= 0.4f)
                {
                    float fadeT = Mathf.InverseLerp(0.4f, 1f, t);
                    color.a = fadeT;
                }
            }
            else
            {
                if (t >= 0.4f)
                {
                    float fadeT = Mathf.InverseLerp(0.4f, 1f, t);
                    color.a = 1f - fadeT;
                }
                else
                {
                    color.a = 1f;
                }
            }

            fadeImage.color = color;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
        Color finalColor = fadeImage.color;
        finalColor.a = isOut ? 1f : 0f;
        fadeImage.color = finalColor;
    }

}
