using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance; // 싱글톤 인스턴스

    public AudioSource musicSource;      // 배경음악을 재생할 AudioSource
    public GameObject audioSourcePrefab; // 효과음 오디오 소스 풀을 생성할 프리팹
    public int poolSize = 10;            // 효과음 오디오 소스 풀의 크기

    private List<AudioSource> sfxPool;   // 효과음 오디오 소스 풀
    private int currentSFXIndex = 0;     // 현재 사용할 오디오 소스 풀의 인덱스

    public AudioClip[] musicClips;       // 배경음악으로 사용할 오디오 클립 배열
    public AudioClip[] sfxClips;         // 효과음으로 사용할 오디오 클립 배열

    private bool isMusicMuted = false;   // 배경음악 음소거 상태를 저장하는 필드
    private bool isSFXMuted = false;     // 효과음 음소거 상태를 저장하는 필드
    private bool isTotalMuted = false;   // 전체 볼륨 음소거 상태를 저장하는 필드

    private float musicVolume = 0.5f;    // 배경음악 볼륨, 기본값은 50%
    private float sfxVolume = 0.5f;      // 효과음 볼륨, 기본값은 50%
    private float totalVolume = 0.5f;    // 전체 볼륨, 기본값은 50%

    // --------------------------------------------------

    // Public 속성 추가
    public float TotalVolume              // 전체 볼륨
    {
        get { return totalVolume; }
        set { SetTotalVolume(value); }
    }

    public float MusicVolume              // 배경 볼륨
    {
        get { return musicVolume; }
        set { SetMusicVolume(value); }
    }

    public float SFXVolume                // 효과음 볼륨
    {
        get { return sfxVolume; }
        set { SetSFXVolume(value); }
    }

    public bool IsTotalMuted              // 전체 볼륨 음소거 여부
    {
        get { return isTotalMuted; }
    }

    public bool IsMusicMuted              // 배경 볼륨 음소거 여부
    {
        get { return isMusicMuted; }
    }

    public bool IsSFXMuted                // 효과음 볼륨 음소거 여부
    {
        get { return isSFXMuted; }
    }


    // 초기화 메서드
    private void Awake()
    {
        // 싱글톤 패턴을 통해 SoundManager 인스턴스가 유일하도록 설정
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // 씬 전환 시에도 SoundManager를 유지

            InitializeSFXPool(); // 효과음 오디오 소스 풀 초기화

            // 저장된 음소거 상태를 PlayerPrefs에서 불러오기
            isMusicMuted = PlayerPrefs.GetInt("MusicMuted", 0) == 1;
            isSFXMuted = PlayerPrefs.GetInt("SFXMuted", 0) == 1;
            isTotalMuted = PlayerPrefs.GetInt("TotalMuted", 0) == 1;
            ApplyMuteSettings(); // 음소거 상태 적용

            // 저장된 볼륨 값을 불러오기 (저장된 값이 없으면 기본값 0.5)
            musicVolume = PlayerPrefs.GetFloat("MusicVolume", 0.5f);
            sfxVolume = PlayerPrefs.GetFloat("SFXVolume", 0.5f);
            totalVolume = PlayerPrefs.GetFloat("TotalVolume", 0.5f);

            SetTotalVolume(totalVolume); // 전체 볼륨 설정
        }
        else
        {
            Destroy(gameObject); // 기존 인스턴스가 있으면 새로 생성된 인스턴스를 제거
        }
    }

    // 효과음 오디오 소스 풀 초기화
    private void InitializeSFXPool()
    {
        if (audioSourcePrefab == null)
        {
            return; // 오디오 소스 프리팹이 없으면 초기화하지 않음
        }

        sfxPool = new List<AudioSource>();

        // 풀의 크기만큼 오디오 소스를 생성하여 풀에 추가
        for (int i = 0; i < poolSize; i++)
        {
            GameObject newSource = Instantiate(audioSourcePrefab, transform);
            AudioSource audioSource = newSource.GetComponent<AudioSource>();

            if (audioSource == null)
            {
                return; // AudioSource 컴포넌트가 없으면 초기화하지 않음
            }

            audioSource.volume = sfxVolume * totalVolume; // 초기 볼륨 설정
            sfxPool.Add(audioSource);
        }
    }

    // 배경 음악 재생
    public void PlayMusic(string clipName)
    {
        AudioClip clip = GetClipByName(musicClips, clipName);
        if (clip != null)
        {
            musicSource.clip = clip;
            musicSource.loop = true;  // 무한 루프 설정
            musicSource.Play();
        }
    }

    // 효과음 재생
    public void PlaySFX(string clipName)
    {
        AudioClip clip = GetClipByName(sfxClips, clipName);
        if (clip != null)
        {
            // 오디오 소스 풀에서 다음 오디오 소스 가져오기
            AudioSource currentSource = sfxPool[currentSFXIndex];
            currentSource.clip = clip;
            currentSource.Play();

            // 다음 인덱스로 이동, 풀의 끝에 도달하면 다시 처음으로
            currentSFXIndex = (currentSFXIndex + 1) % poolSize;
        }
    }

    // 특정 이름의 오디오 클립을 배열에서 찾음
    private AudioClip GetClipByName(AudioClip[] clips, string clipName)
    {
        foreach (AudioClip clip in clips)
        {
            if (clip.name == clipName)
                return clip;
        }
        return null;
    }

    // 배경음악 정지
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // 효과음 정지
    public void StopAllSFX()
    {
        foreach (AudioSource source in sfxPool)
        {
            source.Stop();
        }
    }

    #region 볼륨 설정
    // 전체 볼륨 설정 메서드
    public void SetTotalVolume(float volume)
    {
        totalVolume = volume;

        // 전체 볼륨과 각 개별 볼륨을 반영한 실제 배경음악과 효과음 볼륨 설정
        musicSource.volume = musicVolume * totalVolume;

        foreach (AudioSource source in sfxPool)
        {
            source.volume = sfxVolume * totalVolume;
        }

        // 전체 볼륨 값을 PlayerPrefs에 저장
        PlayerPrefs.SetFloat("TotalVolume", totalVolume);
    }

    // 배경음악 볼륨 설정 메서드
    public void SetMusicVolume(float volume)
    {
        musicVolume = volume;
        musicSource.volume = musicVolume * totalVolume; // 전체 볼륨과 개별 배경음악 볼륨 반영
        PlayerPrefs.SetFloat("MusicVolume", musicVolume); // 배경음악 볼륨 값을 PlayerPrefs에 저장
    }

    // 효과음 볼륨 설정 메서드
    public void SetSFXVolume(float volume)
    {
        sfxVolume = volume;

        // 풀 내 모든 효과음 오디오 소스의 볼륨을 조절
        foreach (AudioSource source in sfxPool)
        {
            source.volume = sfxVolume * totalVolume; // 전체 볼륨과 개별 효과음 볼륨 반영
        }
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume); // 효과음 볼륨 값을 PlayerPrefs에 저장
    }
    #endregion

    #region 음소거
    // 전체 볼륨 음소거 설정
    public void ToggleTotalMute(bool isMuted)
    {
        isTotalMuted = isMuted;

        // 전체 음소거 상태에 맞춰 배경음악과 효과음 음소거 상태를 적용
        ApplyMuteSettings();

        // 전체 음소거 상태를 PlayerPrefs에 저장
        PlayerPrefs.SetInt("TotalMuted", isTotalMuted ? 1 : 0);
    }

    // 배경음악 음소거 설정
    public void ToggleMusicMute(bool isMuted)
    {
        isMusicMuted = isMuted;
        musicSource.mute = isMusicMuted;
        PlayerPrefs.SetInt("MusicMuted", isMusicMuted ? 1 : 0); // 음소거 상태 저장
    }

    // 효과음 음소거 설정
    public void ToggleSFXMute(bool isMuted)
    {
        isSFXMuted = isMuted;

        foreach (AudioSource source in sfxPool)
        {
            source.mute = isSFXMuted;
        }
        PlayerPrefs.SetInt("SFXMuted", isSFXMuted ? 1 : 0);
    }

    // 음소거 상태 적용
    private void ApplyMuteSettings()
    {
        // 전체 음소거 상태에 따라 배경음악과 효과음 음소거 상태 적용
        musicSource.mute = isTotalMuted || isMusicMuted;

        foreach (AudioSource source in sfxPool)
        {
            source.mute = isTotalMuted || isSFXMuted;
        }
    }
    #endregion
}