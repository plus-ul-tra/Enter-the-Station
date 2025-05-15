using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// 스페이스를 누를 때 실행되는 트윈입니다.
/// </summary>
public class T_PunchOnSpace : MonoBehaviour
{
    public RectTransform targetRect;           // 대상 이미지 (Image의 RectTransform)
    public GameObject prefab;                            // 생성할 프리팹
    public Vector2 xRange = new Vector2(-275f, 275f);    // X 좌표 범위
    public Vector2 yRange = new Vector2(-150f, 150f);    // Y 좌표 범위
    public Vector3 punchStrength = new Vector3(0.3f, 0.3f, 0f);  // 얼마나 튀어나올지
    public float punchDuration = 0.3f;          // 애니메이션 지속 시간

    private Tween punchTween;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayPunch();
        }
    }

    private void PlayPunch()
    {
        // 기존 트윈 중단
        punchTween?.Kill();

        // 크기 초기화 (중첩 효과 방지, 부드럽게 돌아가게 하고 싶으면 DOScale 사용)
        targetRect.localScale = Vector3.one;

        // 프리팹 생성
        GameObject instance = Instantiate(prefab, this.transform.parent);

        // 랜덤 위치 설정 (로컬 좌표 기준)
        float randomX = Random.Range(xRange.x, xRange.y);
        float randomY = Random.Range(yRange.x, yRange.y);
        instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);

        // 펀치 애니메이션 실행
        punchTween = targetRect.DOPunchScale(punchStrength, punchDuration)
                               .SetEase(Ease.OutBack)
                               .OnComplete(() =>
                               {
                                   // 애니메이션 끝나고 크기 리셋
                                   targetRect.localScale = Vector3.one;
                               });
    }

    private void OnDisable()
    {
        punchTween?.Kill();
        punchTween = null;
    }
}
