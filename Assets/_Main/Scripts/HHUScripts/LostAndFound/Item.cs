using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemDataSO itemData;
    [SerializeField]
    private SpriteRenderer sprite;
    private float lifeTime = 10.0f;
    private float blinkStart = 6.0f;
    private Tween blinkTween;

    private void OnEnable()
    {
        sprite.enabled = true;
        DOVirtual.DelayedCall(blinkStart, StartBlinking);

        // 전체 수명 뒤 파괴
        DOVirtual.DelayedCall(lifeTime, () =>
        {
            blinkTween?.Kill();
            Destroy(gameObject); // 또는 풀링 반환
        });
    }
    //private void StartBlinking()
    //{
    //    float blinkDuration = lifeTime - blinkStart;

    //    // DOTween Sequence로 깜빡임 빈도 증가
    //    blinkTween = DOTween.To(() => 0.5f, interval =>
    //    {
    //        sprite.DOFade(0, interval / 2f).SetLoops(2, LoopType.Yoyo);
    //    }, 0.5f, blinkDuration).SetEase(Ease.Linear);
    //}
    private void StartBlinking()
    {
        float totalBlinkTime = lifeTime - blinkStart;
        //float elapsed = 0f;
        float minInterval = 0.05f;
        float maxInterval = 0.5f;

        StartCoroutine(BlinkRoutine(totalBlinkTime, minInterval, maxInterval));
    }

    private IEnumerator BlinkRoutine(float duration, float minInterval, float maxInterval)
    {
        float elapsed = 0f;

        while (elapsed < duration)
        {
            // 점점 깜빡이는 속도 증가
            float t = elapsed / duration;
            float interval = Mathf.Lerp(maxInterval, minInterval, t);

            sprite.DOFade(0, interval / 2f);
            yield return new WaitForSeconds(interval / 2f);
            sprite.DOFade(1, interval / 2f);
            yield return new WaitForSeconds(interval / 2f);

            elapsed += interval;
        }

        sprite.DOFade(1, 0); // 완전히 복원 (안정성용)
    }

    public void SetUp(ItemDataSO data)
    {
        itemData = data;
        sprite.sprite = data.icon;
    }

    public void Picked()
    {
        //Debug.Log("줍줍");
        Destroy(gameObject);
    }
}
