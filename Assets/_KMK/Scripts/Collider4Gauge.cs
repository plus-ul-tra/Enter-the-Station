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
        onTriggerEnter.Invoke(); // ReachScope�� ���� �ִ� ��ü�� isReached�� true�� �ٲ�
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit.Invoke(); // ReachScope�� ���� �ִ� ��ü�� isReached�� false�� �ٲ�
    }
}
