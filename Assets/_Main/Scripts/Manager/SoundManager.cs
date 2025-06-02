using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // �̱��� �ν��Ͻ�

    public AudioSource musicSource;      // ��������� ����� AudioSource
    public GameObject audioSourcePrefab; // ȿ���� ����� �ҽ� Ǯ�� ������ ������
    public int poolSize = 10;            // ȿ���� ����� �ҽ� Ǯ�� ũ��

    private List<AudioSource> sfxPool;   // ȿ���� ����� �ҽ� Ǯ
    private int currentSFXIndex = 0;     // ���� ����� ����� �ҽ� Ǯ�� �ε���

    public AudioClip[] musicClips;       // ����������� ����� ����� Ŭ�� �迭
    public AudioClip[] sfxClips;         // ȿ�������� ����� ����� Ŭ�� �迭

    private bool isMusicMuted = false;   // ������� ���Ұ� ���¸� �����ϴ� �ʵ�
    private bool isSFXMuted = false;     // ȿ���� ���Ұ� ���¸� �����ϴ� �ʵ�
    private bool isTotalMuted = false;   // ��ü ���� ���Ұ� ���¸� �����ϴ� �ʵ�

    private float musicVolume = 0.5f;    // ������� ����, �⺻���� 50%
    private float sfxVolume = 0.5f;      // ȿ���� ����, �⺻���� 50%
    private float totalVolume = 0.5f;    // ��ü ����, �⺻���� 50%

    // --------------------------------------------------

    // Public �Ӽ� �߰�
    public float TotalVolume              // ��ü ����
    {
        get { return totalVolume; }
        set { SetTotalVolume(value); }
    }

    public float MusicVolume              // ��� ����
    {
        get { return musicVolume; }
        set { SetMusicVolume(value); }
    }

    public float SFXVolume                // ȿ���� ����
    {
        get { return sfxVolume; }
        set { SetSFXVolume(value); }
    }

    public bool IsTotalMuted              // ��ü ���� ���Ұ� ����
    {
        get { return isTotalMuted; }
    }

    public bool IsMusicMuted              // ��� ���� ���Ұ� ����
    {
        get { return isMusicMuted; }
    }

    public bool IsSFXMuted                // ȿ���� ���� ���Ұ� ����
    {
        get { return isSFXMuted; }
    }


    // �ʱ�ȭ �޼���
    private void Awake()
    {
        // �̱��� ������ ���� SoundManager �ν��Ͻ��� �����ϵ��� ����
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ȯ �ÿ��� SoundManager�� ����

            InitializeSFXPool(); // ȿ���� ����� �ҽ� Ǯ �ʱ�ȭ

            // ����� ���Ұ� ���¸� PlayerPrefs���� �ҷ�����
            isMusicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
            isSFXMuted = PlayerPrefs.GetInt("SFXMuted", 0) == 1;
            isTotalMuted = PlayerPrefs.GetInt("TotalMuted", 0) == 1;
            ApplyMuteSettings(); // ���Ұ� ���� ����

            // ����� ���� ���� �ҷ����� (����� ���� ������ �⺻�� 0.5)
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            totalVolume = PlayerPrefs.GetFloat("TotalVolume", 0.5f);

            SetTotalVolume(totalVolume); // ��ü ���� ����
        }
        else
        {
            Destroy(gameObject); // ���� �ν��Ͻ��� ������ ���� ������ �ν��Ͻ��� ����
        }
    }

    // ȿ���� ����� �ҽ� Ǯ �ʱ�ȭ
    private void InitializeSFXPool()
    {
        if (audioSourcePrefab == null)
        {
            return; // ����� �ҽ� �������� ������ �ʱ�ȭ���� ����
        }

        sfxPool = new List<AudioSource>();

        // Ǯ�� ũ�⸸ŭ ����� �ҽ��� �����Ͽ� Ǯ�� �߰�
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newSource = Instantiate(audioSourcePrefab, transform);
            AudioSource audioSource = newSource.GetComponent<AudioSource>();

            if (audioSource == null)
            {
                return; // AudioSource ������Ʈ�� ������ �ʱ�ȭ���� ����
            }

            audioSource.volume = sfxVolume * totalVolume; // �ʱ� ���� ����
            sfxPool.Add(audioSource);
        }
    }

    // ��� ���� ���
    public void PlayMusic(string clipName)
    {
        AudioClip clip = GetClipByName(musicClips, clipName);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;  // ���� ���� ����
            musicSource.Play();
        }
    }

    // ȿ���� ���
    public void PlaySFX(string clipName)
    {
        AudioClip clip = GetClipByName(sfxClips, clipName);
        if (clip != null)
        {
            // ����� �ҽ� Ǯ���� ���� ����� �ҽ� ��������
            AudioSource currentSource = sfxPool[currentSFXIndex];
            currentSource.clip = clip;
            currentSource.Play();

            // ���� �ε����� �̵�, Ǯ�� ���� �����ϸ� �ٽ� ó������
            currentSFXIndex = (currentSFXIndex + 1) % poolSize;
        }
    }

    // Ư�� �̸��� ����� Ŭ���� �迭���� ã��
    private AudioClip GetClipByName(AudioClip[] clips, string clipName)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == clipName)
                return clip;
        }
        return null;
    }

    // ������� ����
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // ȿ���� ����
    public void StopAllSFX()
    {
        foreach (AudioSource source in sfxPool)
        {
            source.Stop();
        }
    }

    #region ���� ����
    // ��ü ���� ���� �޼���
    public void SetTotalVolume(float volume)
    {
        totalVolume = volume;

        // ��ü ������ �� ���� ������ �ݿ��� ���� ������ǰ� ȿ���� ���� ����
        musicSource.volume = musicVolume * totalVolume;

        foreach (AudioSource source in sfxPool)
        {
            source.volume = sfxVolume * totalVolume;
        }

        // ��ü ���� ���� PlayerPrefs�� ����
        PlayerPrefs.SetFloat("TotalVolume", totalVolume);
    }

    // ������� ���� ���� �޼���
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume * totalVolume; // ��ü ������ ���� ������� ���� �ݿ�
        PlayerPrefs.SetFloat("MusicVolume", musicVolume); // ������� ���� ���� PlayerPrefs�� ����
    }

    // ȿ���� ���� ���� �޼���
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;

        // Ǯ �� ��� ȿ���� ����� �ҽ��� ������ ����
        foreach (AudioSource source in sfxPool)
        {
            source.volume = sfxVolume * totalVolume; // ��ü ������ ���� ȿ���� ���� �ݿ�
        }
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume); // ȿ���� ���� ���� PlayerPrefs�� ����
    }
    #endregion

    #region ���Ұ�
    // ��ü ���� ���Ұ� ����
    public void ToggleTotalMute(bool isMuted)
    {
        isTotalMuted = isMuted;

        // ��ü ���Ұ� ���¿� ���� ������ǰ� ȿ���� ���Ұ� ���¸� ����
        ApplyMuteSettings();

        // ��ü ���Ұ� ���¸� PlayerPrefs�� ����
        PlayerPrefs.SetInt("TotalMuted", isTotalMuted ? 1 : 0);
    }

    // ������� ���Ұ� ����
    public void ToggleMusicMute(bool isMuted)
    {
        isMusicMuted = isMuted;
        musicSource.mute = isMusicMuted;
        PlayerPrefs.SetInt("MusicMuted", isMusicMuted ? 1 : 0); // ���Ұ� ���� ����
    }

    // ȿ���� ���Ұ� ����
    public void ToggleSFXMute(bool isMuted)
    {
        isSFXMuted = isMuted;

        foreach (AudioSource source in sfxPool)
        {
            source.mute = isSFXMuted;
        }
        PlayerPrefs.SetInt("SFXMuted", isSFXMuted ? 1 : 0);
    }

    // ���Ұ� ���� ����
    private void ApplyMuteSettings()
    {
        // ��ü ���Ұ� ���¿� ���� ������ǰ� ȿ���� ���Ұ� ���� ����
        musicSource.mute = isTotalMuted || isMusicMuted;

        foreach (AudioSource source in sfxPool)
        {
            source.mute = isTotalMuted || isSFXMuted;
        }
    }
    #endregion
}