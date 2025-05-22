using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class IntroMoveCamera : MonoBehaviour
{
    [Header("�̵� ����")]
    public float startX = -7f;     // ���� X ��ǥ
    public float endX = 6f;     // ���� X ��ǥ

    [Header("�̵� �ð� ����")]
    public float moveDuration = 2f;

    [Header("Y�� �����̵� ��ġ")]
    public float snapY = -14f;

    [Header("���̵� ���� �ð�")]
    [Tooltip("FadeTeleport�� �ѱ� ���̵���/�ƿ� �ð�")]
    public float fadeDuration = 1f;

    //[Header("�ƾ��̸� üũ")]
    //[SerializeField] private bool isCutScene;

    [Header("���̵� �̹���")]
    public Image fadeImage;

    void Start()
    {
        // ���� ��ġ ����
        var pos = transform.position;
        pos.x = startX;
        transform.position = pos;
        StartCoroutine(MoveRoutine());
    }

    IEnumerator MoveRoutine()
    {
        // 1) ù ��° X�� �̵�
        yield return StartCoroutine(MoveX(startX, endX, true));

        // 2) ȭ���� ���� ���������� ä����
        //var img = ScreenFader.Instance.GetComponent<Image>();
        //img.color = new Color(0, 0, 0, 1);
        //yield return null;

        //if (!isCutScene)
        //{
        //    StartCoroutine(ScreenFader.Instance.Fade(2f, 0f, 2f));
        //}

        // 3) �����̵� (Y��)
        var p = transform.position;
        p.x = startX;
        p.y = snapY;
        transform.position = p;

        // yield return new WaitForSeconds(1f);

        // 4) �� ��° X�� �̵� (���� ȭ�� ���¿��� ����)
        yield return StartCoroutine(MoveX(startX, endX, false));

    }


    IEnumerator MoveX(float fromX, float toX, bool isOut)
    {
        float speed = 4f; // �ʴ� �̵� �ӵ� (���ϴ� ������ ����)

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
