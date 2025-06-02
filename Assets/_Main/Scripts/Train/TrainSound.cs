using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrainSound : MonoBehaviour
{
    [Header("기차 루프 사운드 AudioSource")]
    public AudioSource trainLoopSource;

    [Header("문 열림/닫힘 SFX AudioSource")]
    public AudioSource doorSfxSource;

    [Header("기차 루프 볼륨 (0~1)")]
    [Range(0f, 1f)] public float trainVolume = 1f;
    [Header("문 SFX 볼륨 (0~1)")]
    [Range(0f, 1f)] public float doorVolume = 1f;

    void Awake()
    {
        // trainLoopSource 할당: 인스펙터 없이도 동일 오브젝트의 AudioSource 활용
        if (trainLoopSource == null)
            trainLoopSource = GetComponent<AudioSource>();

        // doorSfxSource 없으면 생성
        if (doorSfxSource == null)
            doorSfxSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // 기차 사운드 초기 설정 및 재생
        trainLoopSource.volume = trainVolume;
        trainLoopSource.loop = true;
        trainLoopSource.playOnAwake = false;
        PlayTrainSound();    // 계속 재생되도록 하는 함수

        // 문 SFX 설정
        doorSfxSource.volume = doorVolume;
        doorSfxSource.loop = false;
        doorSfxSource.playOnAwake = false;
    }

    /// <summary>
    /// 기차 사운드 재생 (루프) - 사라지면 안 되는 핵심 함수
    /// </summary>
    public void PlayTrainSound()
    {
        if (!trainLoopSource.isPlaying)
            trainLoopSource.Play();
    }

    /// <summary>
    /// 기차 사운드 정지
    /// </summary>
    public void StopTrainSound()
    {
        if (trainLoopSource.isPlaying)
            trainLoopSource.Stop();
    }

    /// <summary>
    /// 문 열림 효과음 재생
    /// </summary>
    public void PlayDoorOpen()
    {
        if (doorSfxSource.clip != null)
            doorSfxSource.PlayOneShot(doorSfxSource.clip, doorVolume);
    }

    /// <summary>
    /// 문 닫힘 효과음 재생
    /// </summary>
    public void PlayDoorClose()
    {
        if (doorSfxSource.clip != null)
            doorSfxSource.PlayOneShot(doorSfxSource.clip, doorVolume);
    }
    /// <summary>
    /// 기차 루프 재생 일시정지
    /// </summary>
    public void PauseTrain()
    {
        if (trainLoopSource.isPlaying)
            trainLoopSource.Pause();
    }

    /// <summary>
    /// 기차 루프 재생 재개
    /// </summary>
    public void UnpauseTrain()
    {
        if (!trainLoopSource.isPlaying)
            trainLoopSource.UnPause();
    }
    /// <summary>
    /// 기차 루프 볼륨 조절
    /// </summary>
    /// <param name="v">0~1</param>
    public void SetTrainVolume(float v)
    {
        trainVolume = Mathf.Clamp01(v);
        trainLoopSource.volume = trainVolume;
    }

    /// <summary>
    /// 문 SFX 볼륨 조절
    /// </summary>
    /// <param name="v">0~1</param>
    public void SetDoorVolume(float v)
    {
        doorVolume = Mathf.Clamp01(v);
        doorSfxSource.volume = doorVolume;
    }
}
