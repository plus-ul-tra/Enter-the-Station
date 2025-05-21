using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Item : MonoBehaviour
{
    private ItemDataSO itemData;
    [SerializeField]
    private SpriteRenderer sprite;
    [SerializeField]
    private float lifeTime = 15.0f;
    private float blinkStart = 6.0f;
    private Tween blinkTween;
    private GameObject interactUI;
    private void OnEnable()
    {
        interactUI = transform.GetChild(0).gameObject;
        interactUI.SetActive(false);

        sprite.enabled = true;
        DOVirtual.DelayedCall(blinkStart, StartBlinking);

        // ��ü ���� �� �ı�
        DOVirtual.DelayedCall(lifeTime, () =>
        {
            blinkTween?.Kill();
            Destroy(gameObject); // �Ǵ� Ǯ�� ��ȯ
        });
    }
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
            // ���� �����̴� �ӵ� ����
            float t = elapsed / duration;
            float interval = Mathf.Lerp(maxInterval, minInterval, t);

            sprite.DOFade(0, interval / 2f);
            yield return new WaitForSeconds(interval / 2f);
            sprite.DOFade(1, interval / 2f);
            yield return new WaitForSeconds(interval / 2f);

            elapsed += interval;
        }

        sprite.DOFade(1, 0); // ������ ���� (��������)
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactUI != null)
            {
                interactUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactUI != null)
            {
                interactUI.SetActive(false);
            }
        }
    }
    public void SetUp(ItemDataSO data)
    {
        itemData = data;
        sprite.sprite = data.icon;
    }

    public void Picked()
    {
        //Debug.Log("����");
        Destroy(gameObject);
    }
}
