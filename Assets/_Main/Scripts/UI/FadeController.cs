using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Cinemachine;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Material fadeMat;        // 페이드 머테리얼
    public Transform player;        // 플레이어 트랜스폼
    public Image fadeImage;         // UI Image 컴포넌트

    private Vector3 playerFaceOffset = new Vector3(0f, 1f, 0f);

    private PlayerAnimator playerAnimator;
    private PlayerController playerController;
    private Sequence seq;

    [Header("튜토리얼인지 체크")]
    [SerializeField] private bool isTutorial = false;

    [Header("줌 인할 Virtual Camera")]
    [SerializeField] private CinemachineCamera vcam;

    [Header("줌 인 시 Orthographic Size 값")]
    [SerializeField] private float zoomSize = 1f;

    [Header("기본 Orthographic Size 값")]
    [SerializeField] private float defaultSize = 5f;

    [Header("줌 인 지속 시간(초)")]
    [SerializeField] private float duration = 1f;

    //---------------------------------------------------

    [Header("셔터")]
    [SerializeField] private UpDownMove shutter;

    [Header("진행 중인 게임 캔버스")]
    [SerializeField] private GameObject canvanObj;

    private void Start()
    {
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerController = player.GetComponent<PlayerController>();

        // 시작값 : 페이드 활짝 열기
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(false);

        // 셔터 열기
        if (shutter != null)
            StartCoroutine(shutter.MoveUp());
    }

    private void Update()
    {
        // 1. 플레이어 위치를 Viewport 좌표로 변환 (0~1)
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(player.position + playerFaceOffset);
        fadeMat.SetVector("_HoleCenter", new Vector4(viewportPos.x, viewportPos.y, 0, 0));

        // 2. RectTransform 비율 보정 (왜곡 방지용)
        RectTransform rt = fadeImage.rectTransform;
        float aspectX = rt.rect.width / rt.rect.height;
        fadeMat.SetVector("_RectAspect", new Vector4(aspectX, 1f, 0, 0));
    }

    /// <summary>
    /// 엔딩 페이드를 연출하는 함수
    /// </summary>
    public void DirectEndingFade(bool isClear)
    {
        // 진행중인 게임 종료
        if (canvanObj != null)
            canvanObj.SetActive(false);

        // 플레이어 이동 금지(?) 안됨...
        if (playerController != null)
            playerController.canMove = false;

        // 플레이어 얼굴 변경
        if (playerAnimator != null)
        {
            if (isClear)
            {
                StartCoroutine(MoveAndResizeCamera());
                // 성공
                playerAnimator.SetClear(true);
            }
            else
            {
                StartCoroutine(MoveAndResizeCamera());
                // 실패
                playerAnimator.SetFail(true);
            }
        }


        // 0. 초기화
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(true);

        // 두트윈 시퀀스 생성
        seq = DOTween.Sequence();

        // 1. 반지름을 2초 동안 0.5까지 줄임
        seq.Append(
            DOTween.To(() => fadeMat.GetFloat("_HoleRadius"),
                       x => fadeMat.SetFloat("_HoleRadius", x),
                       0.3f,
                       2f)
                  .SetEase(Ease.InOutSine)
        );

        // 2. 0.5초 대기
        seq.AppendInterval(0.5f);

        // 3. 완전히 닫힘 (0으로)
        seq.Append(
            DOTween.To(() => fadeMat.GetFloat("_HoleRadius"),
                       x => fadeMat.SetFloat("_HoleRadius", x),
                       0f,
                       0.5f)
                  .SetEase(Ease.InQuad)
        );

        seq.AppendCallback(() =>
        {
            if (shutter != null)
                StartCoroutine(shutter.MoveDown());
        });

        seq.AppendInterval(2f);

        seq.OnComplete(() => {

            Scene currentScene = SceneManager.GetActiveScene();

            if (isClear)
            {
                DOTween.KillAll();

                if (isTutorial)
                {
                    // 튜토리얼에서 1일차로 이동
                    DOTween.KillAll();
                    SceneManager.LoadScene("Day1");
                }
                else
                {
                    if (currentScene.name == "Day1")
                    {
                        SceneManager.LoadScene("Day2t");
                    }
                    else if (currentScene.name == "Day2t")
                    {
                        SceneManager.LoadScene("Day3t");
                    }
                    else if (currentScene.name == "Day3t")
                    {
                        SceneManager.LoadScene("Clear");
                    }
                }
            }
            else
            {
                // 실패 엔딩씬으로 이동
                DOTween.KillAll();

                if (currentScene.name == "Day1")
                {
                    SceneManager.LoadScene("FailEnding_Day1");
                }
                else if (currentScene.name == "Day2t")
                {
                    SceneManager.LoadScene("FailEnding_Day2");
                }
                else if (currentScene.name == "Day3t")
                {
                    SceneManager.LoadScene("FailEnding_Day3");
                }
            }
        });
    }
    private IEnumerator MoveAndResizeCamera()
    {
        // 시작 크기와 목표 크기
        if (vcam == null) yield break;

        float startSize = vcam.Lens.OrthographicSize;
        float endSize = zoomSize;

        float elapsed = 0f;

        // duration 동안 계속 반복
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // 부드럽게 보간
            vcam.Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, t);

            yield return null;
        }

        // 정확히 목표 크기로 맞춰주기
        vcam.Lens.OrthographicSize = endSize;

        // 예: 1초 대기 후 다시 원래 크기로 돌아가고 싶다면
        // yield return new WaitForSeconds(1f);
        // StartCoroutine(ResetCameraSize());
    }

    // 필요하다면 원상복구용 코루틴
    private IEnumerator ResetCameraSize()
    {
        float startSize = vcam.Lens.OrthographicSize;
        float endSize = defaultSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            vcam.Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, t);
            yield return null;
        }

        vcam.Lens.OrthographicSize = endSize;
    }
    private void OnDisable()
    {
        seq?.Kill();
    }
}


