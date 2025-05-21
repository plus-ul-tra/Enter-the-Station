using System.Collections;
using UnityEngine;
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

    [Header("�ƾ��̸� üũ")]
    [SerializeField] private bool isCutScene;
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
        yield return StartCoroutine(MoveX(startX, endX));

        // 2) ȭ���� ���� ���������� ä����
        //var img = ScreenFader.Instance.GetComponent<Image>();
        //img.color = new Color(0, 0, 0, 1);
        //yield return null;

        if (!isCutScene)
        {
            StartCoroutine(ScreenFader.Instance.Fade(2f, 0f, 2f));
        }
        

        // 3) �����̵� (Y��)
        var p = transform.position;
        p.x = startX;
        p.y = snapY;
        transform.position = p;

        yield return new WaitForSeconds(1f);

        // 4) �� ��° X�� �̵� (���� ȭ�� ���¿��� ����)
        yield return StartCoroutine(MoveX(startX, endX));

    }




    IEnumerator MoveX(float fromX, float toX)
    {
        float elapsed = 0f;
        Vector3 startPos = transform.position;
        startPos.x = fromX;
        Vector3 endPos = startPos;
        endPos.x = toX;

        while (elapsed < moveDuration)
        {
            float t = elapsed / moveDuration;
            transform.position = Vector3.Lerp(startPos, endPos, t);
            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = endPos;
    }
}
