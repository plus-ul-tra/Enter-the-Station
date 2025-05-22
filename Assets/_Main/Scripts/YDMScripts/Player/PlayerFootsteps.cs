using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("�߰��� ���� ����(AudioSource)")]
    [SerializeField] private AudioSource footstepSource;

    [Header("�߰��� ������ Ŭ��")]
    [SerializeField] private AudioClip footstepLoopClip;

    [Header("�߰��� ���� ���� (0~1)")]
    [Range(0f, 1f)] public float footstepsVolume = 1f;
    private void Awake()
    {
        if (footstepSource == null)
            footstepSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (SoundManager.Instance != null)
            SetfootstepsVolume(SoundManager.Instance.SFXVolume);
    }

    private void Start()
    {
        footstepSource.loop = true;
        footstepSource.playOnAwake = false;
        footstepSource.volume = footstepsVolume;  // �� ���⿡ �ʱ� ���� ����
    }

    public void PlayfootstepsSound()
    {
        if (!footstepSource.isPlaying)
            footstepSource.Play();
    }
    public void StopfootstepsSound()
    {
        if (footstepSource.isPlaying)
            footstepSource.Stop();
    }

    public void SetfootstepsVolume(float v)
    {
        footstepsVolume = Mathf.Clamp01(v);
        footstepSource.volume = footstepsVolume;
    }
}