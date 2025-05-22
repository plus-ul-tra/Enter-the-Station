using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FailEndingManager : MonoBehaviour
{
    [Header("버튼")]
    [Header("다시하기 버튼")]
    [SerializeField] private Button retry_Button;

    [Header("돌아갈씬 이름")]
    [SerializeField] private string sceneName;

    private void Start()
    {
        // 다시하기 버튼
        retry_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 이전 씬으로 돌아가기 (돌아가기 전에 트윈 정리)
            DOTween.KillAll();

            // TODO : 이전 씬이 뭐지 알아야함
            SceneManager.LoadScene(sceneName); //이전 Scene으로
        });
    }
}
