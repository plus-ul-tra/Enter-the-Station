using UnityEngine;

public class TestScripts : MonoBehaviour
{
    RandomEventObject randomEventObject;

    private void Update()
    {
        // E Ű ������ ��ȣ�ۿ�
        if (Input.GetKeyDown(KeyCode.E) && randomEventObject != null)
        {
            randomEventObject.CompleteInteractEvent(); // �̺�Ʈ ����
            randomEventObject = null; // ���� �ʱ�ȭ
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
