using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PlayerFootsteps : MonoBehaviour
{
    [Header("발걸음 루프 사운드(AudioSource)")]
    [SerializeField] private AudioSource footstepSource;

    [Header("발걸음 루프용 클립")]
    [SerializeField] private AudioClip footstepLoopClip;

    [Header("발걸음 루프 볼륨 (0~1)")]
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
        footstepSource.volume = footstepsVolume;  // ← 여기에 초기 볼륨 설정
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