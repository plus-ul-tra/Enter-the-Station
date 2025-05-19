using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TitleButtonManager : MonoBehaviour
{
    [Header("Ÿ��Ʋ")]
    [Header("�ɼ� ��ư")]
    [SerializeField] private Button option_Button;

    [Header("�÷��� ��ư")]
    [SerializeField] private Button play_Button;

    [Header("���� ��ư")]
    [SerializeField] private Button quit_Button;

    [Header("�ɼ� â")]
    [SerializeField] private GameObject optionUI_Obj;

    [Header("�ɼ�")]
    [Header("ȿ���� �����̴�")]
    [SerializeField] private Slider sfx_Slider;

    [Header("����� �����̴�")]
    [SerializeField] private Slider bgm_Slider;

    [Header("ũ���� ��ư")]
    [SerializeField] private Button credit_Button;

    [Header("���� ȭ������ ��ư")]
    [SerializeField] private Button goToMain_Button;

    [Header("ũ����")]
    [Header("ũ���� â")]
    [SerializeField] private GameObject creditUI_Obj;

    [Header("ũ���� �ݱ�")]
    [SerializeField] private Button creditClose_Button;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Ÿ��Ʋ {
        // �ɼ� ��ư �̺�Ʈ
        option_Button.onClick.AddListener(() =>
        {
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");
            
            // �ɼ� �˾� ����
            optionUI_Obj.SetActive(true);
        });

        // �÷��� ��ư
        play_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // TODO : ������ �ƽ� + Ʃ�丮�� ������ �Ѿ��.
        });

        // ������ ��ư
        quit_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

#if UNITY_EDITOR
            // �����Ϳ��� ���� ���� ���, �÷��� ��� ����
            DOTween.KillAll();
            UnityEditor.EditorApplication.isPlaying = false;
#else
            // ����� ���ӿ����� ���� ����
            DOTween.KillAll();
            Application.Quit();
#endif
        });
        // Ÿ��Ʋ }

        // �ɼ�â {
        // ȿ���� �����̴�
        sfx_Slider.onValueChanged.AddListener(ChangeSFXVolume);

        // ����� �����̴�
        bgm_Slider.onValueChanged.AddListener(ChangeBGMVolume);

        // ũ���� ��ư
        credit_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // ũ���� ȭ�� ����
            creditUI_Obj.SetActive(true);
        });

        // ���� ȭ������ ��ư
        goToMain_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // �ɼ� �˾� �ݱ�
            optionUI_Obj.SetActive(false);
        });
        // �ɼ�â }

        // ũ���� {
        creditClose_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // ũ���� ȭ�� �ݱ�
            creditUI_Obj.SetActive(false);
        });
        // ũ���� }

        // �ʱ�ȭ
        optionUI_Obj.SetActive(false);
        creditUI_Obj.SetActive(false);

        if(SoundManager.Instance != null)
        {
            sfx_Slider.value = SoundManager.Instance.SFXVolume;
            bgm_Slider.value = SoundManager.Instance.MusicVolume;
        }
    }

    /// <summary>
    /// ���� �Ŵ����� BGM ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="value"></param>
    private void ChangeBGMVolume(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetMusicVolume(value);
    }

    /// <summary>
    /// ���� �Ŵ����� SFX ������ �����ϴ� �Լ�
    /// </summary>
    /// <param name="value"></param>
    private void ChangeSFXVolume(float value)
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.SetSFXVolume(value);
    }
}
