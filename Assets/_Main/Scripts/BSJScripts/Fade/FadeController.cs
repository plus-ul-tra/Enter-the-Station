using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Cinemachine;
using System.Collections;

public class FadeController : MonoBehaviour
{
    public Material fadeMat;        // ���̵� ���׸���
    public Transform player;        // �÷��̾� Ʈ������
    public Image fadeImage;         // UI Image ������Ʈ

    private Vector3 playerFaceOffset = new Vector3(0f, 1f, 0f);

    private PlayerAnimator playerAnimator;
    private PlayerController playerController;
    private Sequence seq;

    [Header("Ʃ�丮������ üũ")]
    [SerializeField] private bool isTutorial = false;

    [Header("�� ���� Virtual Camera")]
    [SerializeField] private CinemachineCamera vcam;

    [Header("�� �� �� Orthographic Size ��")]
    [SerializeField] private float zoomSize = 1f;

    [Header("�⺻ Orthographic Size ��")]
    [SerializeField] private float defaultSize = 5f;

    [Header("�� �� ���� �ð�(��)")]
    [SerializeField] private float duration = 1f;

    //---------------------------------------------------

    [Header("����")]
    [SerializeField] private UpDownMove shutter;

    [Header("���� ���� ���� ĵ����")]
    [SerializeField] private GameObject canvanObj;

    private void Start()
    {
        playerAnimator = player.GetComponent<PlayerAnimator>();
        playerController = player.GetComponent<PlayerController>();

        // ���۰� : ���̵� Ȱ¦ ����
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(false);

        // ���� ����
        if (shutter != null)
            StartCoroutine(shutter.MoveUp());
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
    public void DirectEndingFade(bool isClear)
    {
        // �������� ���� ����
        if (canvanObj != null)
            canvanObj.SetActive(false);

        // �÷��̾� �̵� ����(?) �ȵ�...
        if (playerController != null)
            playerController.canMove = false;

        // �÷��̾� �� ����
        if (playerAnimator != null)
        {
            if (isClear)
            {
                StartCoroutine(MoveAndResizeCamera());
                // ����
                playerAnimator.SetClear(true);
            }
            else
            {
                StartCoroutine(MoveAndResizeCamera());
                // ����
                playerAnimator.SetFail(true);
            }
        }


        // 0. �ʱ�ȭ
        fadeMat.SetFloat("_HoleRadius", 2f);
        fadeImage.gameObject.SetActive(true);

        // ��Ʈ�� ������ ����
        seq = DOTween.Sequence();

        // 1. �������� 2�� ���� 0.5���� ����
        seq.Append(
            DOTween.To(() => fadeMat.GetFloat("_HoleRadius"),
                       x => fadeMat.SetFloat("_HoleRadius", x),
                       0.3f,
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
                    // Ʃ�丮�󿡼� 1������ �̵�
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
                // ���� ���������� �̵�
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
        // ���� ũ��� ��ǥ ũ��
        if (vcam == null) yield break;

        float startSize = vcam.Lens.OrthographicSize;
        float endSize = zoomSize;

        float elapsed = 0f;

        // duration ���� ��� �ݺ�
        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);

            // �ε巴�� ����
            vcam.Lens.OrthographicSize = Mathf.Lerp(startSize, endSize, t);

            yield return null;
        }

        // ��Ȯ�� ��ǥ ũ��� �����ֱ�
        vcam.Lens.OrthographicSize = endSize;

        // ��: 1�� ��� �� �ٽ� ���� ũ��� ���ư��� �ʹٸ�
        // yield return new WaitForSeconds(1f);
        // StartCoroutine(ResetCameraSize());
    }

    // �ʿ��ϴٸ� ���󺹱��� �ڷ�ƾ
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


