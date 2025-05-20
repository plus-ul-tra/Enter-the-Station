using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailEndingManager : MonoBehaviour
{
    [Header("��ư")]
    [Header("�ٽ��ϱ� ��ư")]
    [SerializeField] private Button retry_Button;

    private void Start()
    {
        // �ٽ��ϱ� ��ư
        retry_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // ���� ������ ���ư��� (���ư��� ���� Ʈ�� ����)
            DOTween.KillAll();
            SceneManager.LoadScene("BSJ_Test"); //���� Scene����
        });
    }
}
