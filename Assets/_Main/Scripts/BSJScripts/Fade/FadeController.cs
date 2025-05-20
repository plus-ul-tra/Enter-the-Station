using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Material fadeMat;        // ���̵� ���׸���
    public Transform player;        // �÷��̾� Ʈ������
    public Image fadeImage;         // UI Image ������Ʈ

    private Vector3 playerFaceOffset = new Vector3(0f, 0.8f, 0f);
    private Sequence seq;

    [Header("Ʃ�丮������ üũ")]
    [SerializeField] private bool isTutorial = false;

    private void Start()
    {
        // ���۰� : ���̵� Ȱ¦ ����
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        // 1. �÷��̾� ��ġ�� Viewport ��ǥ�� ��ȯ (0~1)
        Vector2 viewportPos = Camera.main.WorldToViewportPoint(player.position + playerFaceOffset);
        fadeMat.SetVector("_HoleCenter", new Vector4(viewportPos.x, viewportPos.y, 0, 0));

        // 2. RectTransform ���� ���� (�ְ� ������)
        RectTransform rt = fadeImage.rectTransform;
        float aspectX = rt.rect.width / rt.rect.height;
        fadeMat.SetVector("_RectAspect", new Vector4(aspectX, 1f, 0, 0));
    }

    /// <summary>
    /// ���� ���̵带 �����ϴ� �Լ�
    /// </summary>
    public void DirectEndingFade()
    {
        // 0. �ʱ�ȭ
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(true);

        // ��Ʈ�� ������ ����
        seq = DOTween.Sequence();

        // 1. �������� 2�� ���� 0.5���� ����
        seq.Append(
            DOTween.To(() => fadeMat.GetFloat("_HoleRadius"),
                       x => fadeMat.SetFloat("_HoleRadius", x),
                       0.1f,
                       2f)
                  .SetEase(Ease.InOutSine)
        );

        // 2. 0.5�� ���
        seq.AppendInterval(0.5f);

        // 3. ������ ���� (0����)
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
                // ���� ���������� �̵�
                DOTween.KillAll();
                SceneManager.LoadScene("Day1");
            }
            else
            {
                // ���� ���������� �̵�
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
