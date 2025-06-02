using UnityEngine;

public class UIEnableSound : MonoBehaviour
{
    private void OnEnable()
    {
        if (SoundManager.Instance != null)
            SoundManager.Instance.PlaySFX("UIButton_sound");
    }
}
