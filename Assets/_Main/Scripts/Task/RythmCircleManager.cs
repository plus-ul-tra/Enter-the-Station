using UnityEngine;

public class RythmCircleManager : MonoBehaviour
{
    public GameObject circle;

    void Update()
    {
        if (circle.activeSelf == false)
        {
            circle.SetActive(true);
        }
    }
}
