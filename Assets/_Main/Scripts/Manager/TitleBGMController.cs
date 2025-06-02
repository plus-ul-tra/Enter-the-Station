using UnityEngine;

public class TitleBGMController : MonoBehaviour
{
    void Start()
    {
        if(SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMusic("Title_bgm");
        }
    }
}
