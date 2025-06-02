using UnityEngine;

public class InGameBGMController : MonoBehaviour
{
    void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayMusic("Ingame_bgm");
        }
    }
}
