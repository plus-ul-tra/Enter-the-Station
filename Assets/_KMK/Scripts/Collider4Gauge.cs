using UnityEngine;

public class Collider4Gauge : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        ReachScope.isReached = true;
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        ReachScope.isReached = false;
    }
}
