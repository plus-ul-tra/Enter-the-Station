using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [Header("비디오 플레이어")]
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        // 비디오가 끝나면 호출될 이벤트 등록
        videoPlayer.loopPointReached += OnVideoEnd;

        // 비디오 재생
        videoPlayer.Play();
    }

    // 비디오가 끝났을 때 호출될 메서드
    private void OnVideoEnd(VideoPlayer vp)
    {
        // 다음 씬으로 이동
        SceneManager.LoadScene("Tutorial");
    }
}
