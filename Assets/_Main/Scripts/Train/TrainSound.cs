using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class TrainSound : MonoBehaviour
{
    [Header("���� ���� ���� AudioSource")]
    public AudioSource trainLoopSource;

    [Header("�� ����/���� SFX AudioSource")]
    public AudioSource doorSfxSource;

    [Header("���� ���� ���� (0~1)")]
    [Range(0f, 1f)] public float trainVolume = 1f;
    [Header("�� SFX ���� (0~1)")]
    [Range(0f, 1f)] public float doorVolume = 1f;

    void Awake()
    {
        // trainLoopSource �Ҵ�: �ν����� ���̵� ���� ������Ʈ�� AudioSource Ȱ��
        if (trainLoopSource == null)
            trainLoopSource = GetComponent<AudioSource>();

        // doorSfxSource ������ ����
        if (doorSfxSource == null)
            doorSfxSource = gameObject.AddComponent<AudioSource>();
    }

    void Start()
    {
        // ���� ���� �ʱ� ���� �� ���
        trainLoopSource.volume = trainVolume;
        trainLoopSource.loop = true;
        trainLoopSource.playOnAwake = false;
        PlayTrainSound();    // ��� ����ǵ��� �ϴ� �Լ�

        // �� SFX ����
        doorSfxSource.volume = doorVolume;
        doorSfxSource.loop = false;
        doorSfxSource.playOnAwake = false;
    }

    /// <summary>
    /// ���� ���� ��� (����) - ������� �� �Ǵ� �ٽ� �Լ�
    /// </summary>
    public void PlayTrainSound()
    {
        if (!trainLoopSource.isPlaying)
            trainLoopSource.Play();
    }

    /// <summary>
    /// ���� ���� ����
    /// </summary>
    public void StopTrainSound()
    {
        if (trainLoopSource.isPlaying)
            trainLoopSource.Stop();
    }

    /// <summary>
    /// �� ���� ȿ���� ���
    /// </summary>
    public void PlayDoorOpen()
    {
        if (doorSfxSource.clip != null)
            doorSfxSource.PlayOneShot(doorSfxSource.clip, doorVolume);
    }

    /// <summary>
    /// �� ���� ȿ���� ���
    /// </summary>
    public void PlayDoorClose()
    {
        if (doorSfxSource.clip != null)
            doorSfxSource.PlayOneShot(doorSfxSource.clip, doorVolume);
    }
    /// <summary>
    /// ���� ���� ��� �Ͻ�����
    /// </summary>
    public void PauseTrain()
    {
        if (trainLoopSource.isPlaying)
            trainLoopSource.Pause();
    }

    /// <summary>
    /// ���� ���� ��� �簳
    /// </summary>
    public void UnpauseTrain()
    {
        if (!trainLoopSource.isPlaying)
            trainLoopSource.UnPause();
    }
    /// <summary>
    /// ���� ���� ���� ����
    /// </summary>
    /// <param name="v">0~1</param>
    public void SetTrainVolume(float v)
    {
        trainVolume = Mathf.Clamp01(v);
        trainLoopSource.volume = trainVolume;
    }

    /// <summary>
    /// �� SFX ���� ����
    /// </summary>
    /// <param name="v">0~1</param>
    public void SetDoorVolume(float v)
    {
        doorVolume = Mathf.Clamp01(v);
        doorSfxSource.volume = doorVolume;
    }
}
