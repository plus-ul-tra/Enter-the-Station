using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameOptionManager : MonoBehaviour
{
    [Header("버튼")]
    [Header("계속하기 버튼")]
    [SerializeField] private Button continue_Button;

    [Header("다시하기 버튼")]
    [SerializeField] private Button retry_Button;

    [Header("메인메뉴로 버튼")]
    [SerializeField] private Button mainMenu_Button;

    [Header("볼륨 슬라이더")]
    [Header("BGM 슬라이더")]
    [SerializeField] private Slider bgm_Slider;

    [Header("SFX 슬라이더")]
    [SerializeField] private Slider sfx_Slider;

    [Header("옵션 창")]
    [SerializeField] private GameObject optionUI_Obj;

    private void Start()
    {
        // 초기화
        optionUI_Obj.SetActive(false);

        if (SoundManager.Instance != null)
        {
            sfx_Slider.value = SoundManager.Instance.SFXVolume;
            bgm_Slider.value = SoundManager.Instance.MusicVolume;
        }

        // 버튼 이벤트 {
        // 계속하기 버튼
        continue_Button.onClick.AddListener(() =>
        {
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 옵션 창 닫기
            optionUI_Obj.SetActive(false);

            // 게임 속도 1
            Time.timeScale = 1f;
        });

        // 재시작 버튼
        retry_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 옵션 창 닫기
            optionUI_Obj.SetActive(false);

            // 게임 속도 1
            Time.timeScale = 1f;

            // 씬 재시작 ( 재시작 전에 실행 중인 트윈 코드 정리 )
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        // 메인 메뉴로 가기 버튼
        mainMenu_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 옵션 창 닫기
            optionUI_Obj.SetActive(false);

            // 게임 속도 1
            Time.timeScale = 1f;

            // 타이틀 씬으로 이동 ( 재시작 전에 실행 중인 트윈 코드 정리 )
            DOTween.KillAll();
            SceneManager.LoadScene("BSJ_Title");
        });
        // 버튼 이벤트 }

        // 배경음 슬라이더
        bgm_Slider.onValueChanged.AddListener(ChangeBGMVolume);

        // 효과음 슬라이더
        sfx_Slider.onValueChanged.AddListener(ChangeSFXVolume);
    }

    /// <summary>
    /// 사운드 매니저의 BGM 볼륨을 조절하는 함수
    /// </summary>
    /// <param name="value"></param>
    private void ChangeBGMVolume(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetMusicVolume(value);
    }

    /// <summary>
    /// 사운드 매니저의 SFX 볼륨을 조절하는 함수
    /// </summary>
    /// <param name="value"></param>
    private void ChangeSFXVolume(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetSFXVolume(value);
    }
}
