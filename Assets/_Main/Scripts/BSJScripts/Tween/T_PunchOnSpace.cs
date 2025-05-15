using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// �����̽��� ���� �� ����Ǵ� Ʈ���Դϴ�.
/// </summary>
public class T_PunchOnSpace : MonoBehaviour
{
    public RectTransform targetRect;           // ��� �̹��� (Image�� RectTransform)
    public GameObject prefab;                            // ������ ������
    public Vector2 xRange = new Vector2(-275f, 275f);    // X ��ǥ ����
    public Vector2 yRange = new Vector2(-150f, 150f);    // Y ��ǥ ����
    public Vector3 punchStrength = new Vector3(0.3f, 0.3f, 0f);  // �󸶳� Ƣ�����
    public float punchDuration = 0.3f;          // �ִϸ��̼� ���� �ð�

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
        // ���� Ʈ�� �ߴ�
        punchTween?.Kill();

        // ũ�� �ʱ�ȭ (��ø ȿ�� ����, �ε巴�� ���ư��� �ϰ� ������ DOScale ���)
        targetRect.localScale = Vector3.one;

        // ������ ����
        GameObject instance = Instantiate(prefab, this.transform.parent);

        // ���� ��ġ ���� (���� ��ǥ ����)
        float randomX = Random.Range(xRange.x, xRange.y);
        float randomY = Random.Range(yRange.x, yRange.y);
        instance.GetComponent<RectTransform>().anchoredPosition = new Vector2(randomX, randomY);

        // ��ġ �ִϸ��̼� ����
        punchTween = targetRect.DOPunchScale(punchStrength, punchDuration)
                               .SetEase(Ease.OutBack)
                               .OnComplete(() =>
                               {
                                   // �ִϸ��̼� ������ ũ�� ����
                                   targetRect.localScale = Vector3.one;
                               });
    }

    private void OnDisable()
    {
        punchTween?.Kill();
        punchTween = null;
    }
}
