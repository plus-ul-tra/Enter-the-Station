using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroManager : MonoBehaviour
{
    [Header("���� �÷��̾�")]
    [SerializeField] private VideoPlayer videoPlayer;

    private void Start()
    {
        // ������ ������ ȣ��� �̺�Ʈ ���
        videoPlayer.loopPointReached += OnVideoEnd;

        // ���� ���
        videoPlayer.Play();
    }

    // ������ ������ �� ȣ��� �޼���
    private void OnVideoEnd(VideoPlayer vp)
    {
        // ���� ������ �̵�
        SceneManager.LoadScene("Tutorial");
    }
}
