using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailEndingManager : MonoBehaviour
{
    [Header("버튼")]
    [Header("다시하기 버튼")]
    [SerializeField] private Button retry_Button;

    private void Start()
    {
        // 다시하기 버튼
        retry_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 이전 씬으로 돌아가기 (돌아가기 전에 트윈 정리)
            DOTween.KillAll();
            SceneManager.LoadScene("BSJ_Test"); //이전 Scene으로
        });
    }
}
