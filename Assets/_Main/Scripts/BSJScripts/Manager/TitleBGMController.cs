using UnityEngine;

public class TitleBGMController : MonoBehaviour
{
    void Start()
    {
        SoundManager.Instance.PlayMusic("Title_bgm");
    }
}
