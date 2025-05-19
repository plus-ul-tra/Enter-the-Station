using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IngameOptionManager : MonoBehaviour
{
    [Header("��ư")]
    [Header("����ϱ� ��ư")]
    [SerializeField] private Button continue_Button;

    [Header("�ٽ��ϱ� ��ư")]
    [SerializeField] private Button retry_Button;

    [Header("���θ޴��� ��ư")]
    [SerializeField] private Button mainMenu_Button;

    [Header("���� �����̴�")]
    [Header("BGM �����̴�")]
    [SerializeField] private Slider bgm_Slider;

    [Header("SFX �����̴�")]
    [SerializeField] private Slider sfx_Slider;

    [Header("�ɼ� â")]
    [SerializeField] private GameObject optionUI_Obj;

    private void Start()
    {
        // �ʱ�ȭ
        optionUI_Obj.SetActive(false);

        if (SoundManager.Instance != null)
        {
            sfx_Slider.value = SoundManager.Instance.SFXVolume;
            bgm_Slider.value = SoundManager.Instance.MusicVolume;
        }

        // ��ư �̺�Ʈ {
        // ����ϱ� ��ư
        continue_Button.onClick.AddListener(() =>
        {
            if(SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // �ɼ� â �ݱ�
            optionUI_Obj.SetActive(false);

            // ���� �ӵ� 1
            Time.timeScale = 1f;
        });

        // ����� ��ư
        retry_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // �ɼ� â �ݱ�
            optionUI_Obj.SetActive(false);

            // ���� �ӵ� 1
            Time.timeScale = 1f;

            // �� ����� ( ����� ���� ���� ���� Ʈ�� �ڵ� ���� )
            DOTween.KillAll();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        });

        // ���� �޴��� ���� ��ư
        mainMenu_Button.onClick.AddListener(() =>
        {
            if (SoundManager.Instance != null)
                SoundManager.Instance.PlaySFX("UIButton_sound");

            // �ɼ� â �ݱ�
            optionUI_Obj.SetActive(false);

            // ���� �ӵ� 1
            Time.timeScale = 1f;

            // Ÿ��Ʋ ������ �̵� ( ����� ���� ���� ���� Ʈ�� �ڵ� ���� )
            DOTween.KillAll();
            SceneManager.LoadScene("BSJ_Title");
        });
        // ��ư �̺�Ʈ }

        // ����� �����̴�
        bgm_Slider.onValueChanged.AddListener(ChangeBGMVolume);

        // ȿ���� �����̴�
        sfx_Slider.onValueChanged.AddListener(ChangeSFXVolume);
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
