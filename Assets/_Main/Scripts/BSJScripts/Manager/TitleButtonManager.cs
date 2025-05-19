using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    [Header("타이틀")]
    [Header("옵션 버튼")]
    [SerializeField] private Button option_Button;

    [Header("플레이 버튼")]
    [SerializeField] private Button play_Button;

    [Header("종료 버튼")]
    [SerializeField] private Button quit_Button;

    [Header("옵션 창")]
    [SerializeField] private GameObject optionUI_Obj;

    [Header("옵션")]
    [Header("효과음 슬라이더")]
    [SerializeField] private Slider sfx_Slider;

    [Header("배경음 슬라이더")]
    [SerializeField] private Slider bgm_Slider;

    [Header("크레딧 버튼")]
    [SerializeField] private Button credit_Button;

    [Header("메인 화면으로 버튼")]
    [SerializeField] private Button goToMain_Button;

    [Header("크레딧")]
    [Header("크레딧 창")]
    [SerializeField] private GameObject creditUI_Obj;

    [Header("크레딧 닫기")]
    [SerializeField] private Button creditClose_Button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // 타이틀 {
        // 옵션 버튼 이벤트
        option_Button.onClick.AddListener(() =>
        {
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");
            
            // 옵션 팝업 열기
            optionUI_Obj.SetActive(true);
        });

        // 플레이 버튼
        play_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // TODO : 오프닝 컷신 + 튜토리얼 씬으로 넘어가기.
        });

        // 나가기 버튼
        quit_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

#if UNITY_EDITOR
            // 에디터에서 실행 중일 경우, 플레이 모드 종료
            DOTween.KillAll();
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // 빌드된 게임에서는 게임 종료
            DOTween.KillAll();
            Application.Quit();
#endif
        });
        // 타이틀 }

        // 옵션창 {
        // 효과음 슬라이더
        sfx_Slider.onValueChanged.AddListener(ChangeSFXVolume);

        // 배경음 슬라이더
        bgm_Slider.onValueChanged.AddListener(ChangeBGMVolume);

        // 크레딧 버튼
        credit_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 크레딧 화면 열기
            creditUI_Obj.SetActive(true);
        });

        // 메인 화면으로 버튼
        goToMain_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 옵션 팝업 닫기
            optionUI_Obj.SetActive(false);
        });
        // 옵션창 }

        // 크레딧 {
        creditClose_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // 크레딧 화면 닫기
            creditUI_Obj.SetActive(false);
        });
        // 크레딧 }

        // 초기화
        optionUI_Obj.SetActive(false);
        creditUI_Obj.SetActive(false);

        if(SoundManager.Instance != null)
        {
            sfx_Slider.value = SoundManager.Instance.SFXVolume;
            bgm_Slider.value = SoundManager.Instance.MusicVolume;
        }
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
