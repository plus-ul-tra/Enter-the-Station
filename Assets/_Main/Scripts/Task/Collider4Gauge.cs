using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class Collider4Gauge : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public GameObject spaceBar;

    void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter.Invoke(); // ReachScope를 갖고 있는 객체의 isReached를 true로 바꿈
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit.Invoke(); // ReachScope를 갖고 있는 객체의 isReached를 false로 바꿈
    }
}
