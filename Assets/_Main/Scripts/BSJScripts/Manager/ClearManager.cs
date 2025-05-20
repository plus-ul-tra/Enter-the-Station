using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearManager : MonoBehaviour
{
    [Header("Ŭ���� �̹���")]
    [SerializeField] private RectTransform clearCutImage;

    [Header("������� �̹���")]
    [SerializeField] private CanvasGroup backFillterImageCG;

    [Header("���� �̹�����")]
    [SerializeField] private T_AnchorMove[] paperImages;

    [Header("��� ����")]
    [SerializeField] private T_AnchorMove resultPaper;

    [Header("����")]
    [SerializeField] private T_StampEffect stamp;

    [Header("�������� ����")]
    [SerializeField] private Button goToMain_Button;
    private void Start()
    {
        backFillterImageCG.alpha = 0f; // ���� ���İ�

        goToMain_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // Ÿ��Ʋ ������ �̵� ( ����� ���� ���� ���� Ʈ�� �ڵ� ���� )
            DOTween.KillAll();
            SceneManager.LoadScene("Title");
        });

        StartCoroutine(ClearEndingCoroutine());
    }

    /// <summary>
    /// Ŭ���� ������ �����ϴ� �ڷ�ƾ
    /// </summary>
    /// <returns></returns>
    private IEnumerator ClearEndingCoroutine()
    {
        // DOTween Sequence ����
        Sequence seq = DOTween.Sequence();

        // 1. Ŭ���� �̹��� ������ ���� (3�� ���� 1 �� 1.1�� Ŀ����)
        seq.Append(clearCutImage.DOScale(Vector3.one * 1.1f, 3f).SetEase(Ease.OutBack));

        // 2. �̹��� ���İ� ���̵� Ʈ�� ���� (1��)
        // 3. ���� �̹����� 0.2�� �������� �ݹ� ���� (���ÿ� �����ϵ��� Join���� ����)
        seq.Append(backFillterImageCG.DOFade(1f, 1f));

        foreach (var paper in paperImages)
        {
            seq.Join(DOTween.Sequence() // ���� ������ ����
                .AppendCallback(() => paper.MoveToTargetPosition())
                .AppendInterval(0.2f));
        }

        // 4. ��� ���� �̵�
        seq.AppendCallback(() => resultPaper.MoveToTargetPosition());
        seq.AppendInterval(0.2f); // 0.2�� ��� �� ���� �̹��� �̵�

        // TODO : ��� Ÿ���� �Ǳ�

        // 5. ������ ���� �������
        seq.AppendInterval(2f);
        seq.AppendCallback(() => {
            stamp.gameObject.SetActive(true);
        });
        seq.AppendInterval(2f); // 2�� ���
        
        yield return seq.WaitForCompletion();

        // ���θ޴� ���� ��ư Ȱ��ȭ
        if (goToMain_Button != null)
            goToMain_Button.gameObject.SetActive(true);

        yield return null;
    }
}
