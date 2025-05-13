using UnityEngine;

public class TestScripts : MonoBehaviour
{
    RandomEventObject randomEventObject;

    private void Update()
    {
        // E 키 눌러서 상호작용
        if (Input.GetKeyDown(KeyCode.E) && randomEventObject != null)
        {
            randomEventObject.CompleteInteractEvent(); // 이벤트 실행
            randomEventObject = null; // 참조 초기화
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("EventObject"))
        {
            randomEventObject = other.GetComponent<RandomEventObject>();
        }
    }
}
