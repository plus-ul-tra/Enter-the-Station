using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Material fadeMat;        // 페이드 머테리얼
    public Transform player;        // 플레이어 트랜스폼
    public Image fadeImage;         // UI Image 컴포넌트

    private Vector3 playerFaceOffset = new Vector3(0f, 0.8f, 0f);
    private Sequence seq;

    [Header("튜토리얼인지 체크")]
    [SerializeField] private bool isTutorial = false;

    private void Start()
    {
        // 시작값 : 페이드 활짝 열기
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(false);
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
    public void DirectEndingFade()
    {
        // 0. 초기화
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(true);

        // 두트윈 시퀀스 생성
        seq = DOTween.Sequence();

        // 1. 반지름을 2초 동안 0.5까지 줄임
        seq.Append(
            DOTween.To(() => fadeMat.GetFloat("_HoleRadius"),
                       x => fadeMat.SetFloat("_HoleRadius", x),
                       0.1f,
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

        seq.OnComplete(() => {
            if(isTutorial)
            {
                // 실패 엔딩씬으로 이동
                DOTween.KillAll();
                SceneManager.LoadScene("Day1");
            }
            else
            {
                // 실패 엔딩씬으로 이동
                DOTween.KillAll();
                SceneManager.LoadScene("FailEnding");
            } 
        });
    }

    private void OnDisable()
    {
        seq?.Kill();
    }
}
