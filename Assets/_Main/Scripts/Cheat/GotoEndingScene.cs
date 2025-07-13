using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoEndingScene : MonoBehaviour
{
    // ΩÃ±€≈Ê ¿ŒΩ∫≈œΩ∫
    public static GotoEndingScene Instance { get; private set; }

    void Awake()
    {
        // ΩÃ±€≈Ê ∆–≈œ ¿˚øÎ
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F1))
        {
            SceneManager.LoadScene("Clear");
        }
    }
}
