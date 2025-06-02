using DG.Tweening;
using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SkipOptionManager : MonoBehaviour
{
    [Header("스킵하기 버튼")]
    [SerializeField] private Button doSkip_Button;

    [Header("스킵 Yes 버튼")]
    [SerializeField] private Button skipYes_Button;

    [Header("스킵 No 버튼")]
    [SerializeField] private Button skipNo_Button;

    [Header("스킵 창")]
    [SerializeField] private GameObject skipUI_Obj;

    [Header("스킵하고 다음으로 갈 씬 이름")]
    [SerializeField] private String sceneName;

    [Header("플레이어 발소리")]
    [SerializeField] private PlayerFootsteps playerFootstpes;

    private void Start()
    {
        // 초기화
        skipUI_Obj.SetActive(false);

        // 스킵하기 버튼
        doSkip_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            if (playerFootstpes != null)
                playerFootstpes.StopfootstepsSound();

            // 게임 중지
            Time.timeScale = 0f;

            // 스킵창 오픈
            skipUI_Obj.SetActive(true);
        });

        // 스킵 수락 버튼
        skipYes_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 게임 중지
            Time.timeScale = 1f;

            // Day1로 이동하기
            DOTween.KillAll();
            SceneManager.LoadScene(sceneName);
        });

        // 스킵 거절 버튼
        skipNo_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 게임 재개
            Time.timeScale = 1f;

            // 스킵창 닫기
            skipUI_Obj.SetActive(false);
        });
    }
}
