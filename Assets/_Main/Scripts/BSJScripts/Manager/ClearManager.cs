using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    [Header("클리어 이미지")]
    [SerializeField] private RectTransform clearCutImage;

    [Header("배경필터 이미지")]
    [SerializeField] private CanvasGroup backFillterImageCG;

    [Header("종이 이미지들")]
    [SerializeField] private T_AnchorMove[] paperImages;

    [Header("결과 종이")]
    [SerializeField] private T_AnchorMove resultPaper;

    [Header("도장")]
    [SerializeField] private T_StampEffect stamp;

    [Header("메인으로 가기")]
    [SerializeField] private Button goToMain_Button;
    private void Start()
    {
        backFillterImageCG.alpha = 0f; // 시작 알파값

        goToMain_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 타이틀 씬으로 이동 ( 재시작 전에 실행 중인 트윈 코드 정리 )
            DOTween.KillAll();
            SceneManager.LoadScene("Title");
        });

        StartCoroutine(ClearEndingCoroutine());
    }

    /// <summary>
    /// 클리어 엔딩을 실행하는 코루틴
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClearEndingCoroutine()
    {
        // DOTween Sequence 생성
        Sequence seq = DOTween.Sequence();

        // 1. 클리어 이미지 스케일 증가 (3초 동안 1 → 1.1로 커지기)
        seq.Append(clearCutImage.DOScale(Vector3.one * 1.1f, 3f).SetEase(Ease.OutBack));

        // 2. 이미지 알파값 페이드 트윈 시작 (1초)
        // 3. 종이 이미지는 0.2초 간격으로 콜백 실행 (동시에 시작하도록 Join으로 묶기)
        seq.Append(backFillterImageCG.DOFade(1f, 1f));

        foreach (var paper in paperImages)
        {
            seq.Join(DOTween.Sequence() // 병렬 시퀀스 생성
                .AppendCallback(() => paper.MoveToTargetPosition())
                .AppendInterval(0.2f));
        }

        // 4. 결과 종이 이동
        seq.AppendCallback(() => resultPaper.MoveToTargetPosition());
        seq.AppendInterval(0.2f); // 0.2초 대기 후 다음 이미지 이동

        // TODO : 결과 타이핑 되기

        // 5. 점수에 따라서 도장찍기
        seq.AppendInterval(2f);
        seq.AppendCallback(() => {
            stamp.gameObject.SetActive(true);
        });
        seq.AppendInterval(2f); // 2초 대기
        
        yield return seq.WaitForCompletion();

        // 메인메뉴 가기 버튼 활성화
        if (goToMain_Button != null)
            goToMain_Button.gameObject.SetActive(true);

        yield return null;
    }
}
