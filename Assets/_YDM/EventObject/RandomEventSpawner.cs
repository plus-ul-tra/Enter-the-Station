using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class RandomEventSpawner : MonoBehaviour
{
    public TaskManager taskManager;                     // �̴ϰ��� ���� �Ŵ���
    public EventDirectionArrow eventDirectionArrow;     // �̺�Ʈ ȭ��ǥ
    public SpeechBubble speechBubble;                   // ������ ��ǳ��

    public List<RandomEventObject> randomEventList;     // ���� ���߻�Ȳ �������� ���� ����Ʈ
    public List<RandomEventObject> createdEventList;    // ������ ���߻�Ȳ ���� ����Ʈ

    [Header("���߻�Ȳ �߻� �ֱ�(�ð�)")]
    [SerializeField]
    private float eventSpawnTime = 15f;
    private float currentSpawnTimer = 0f;               // ���� ���� �ð�

    private void Start()
    {
        // �׽�Ʈ : ������ �� �� �� �Ŀ� ���߻�Ȳ �߻��� ������?
        currentSpawnTimer = 10f;
    }

    private void Update()
    {
        // ���߻�Ȳ �߻� �ֱ� ���� �̺�Ʈ ����
        currentSpawnTimer += Time.deltaTime;

        if(currentSpawnTimer >= eventSpawnTime)
        {
            currentSpawnTimer -= eventSpawnTime;

            // TODO : �׽�Ʈ�� ���� ���� ���߿� �����ؾ���.
            Vector3 randomPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            CreateRandomEventObject(randomPosition);
        }
    }

    /// <summary>
    /// ���� ������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="eventPosition"></param>
    public void CreateRandomEventObject(Vector3 eventPosition)
    {
        int randomIndex = Random.Range(0, randomEventList.Count);

        GameObject eventObject = Instantiate(randomEventList[randomIndex].gameObject, eventPosition, Quaternion.identity);
        if(eventObject.TryGetComponent<RandomEventObject>(out RandomEventObject randomEvent))
        {
            createdEventList.Add(randomEvent);

            // �ݹ� (��ȣ�ۿ� ���� / ����) ���
            randomEvent.onEventSuccess += OnRandomEventSuccess;
            randomEvent.onEventFailed += OnRandomEventInteractFailed;

            // �̴ϰ��� ������ ����
            randomEvent.ReferTaskManager(taskManager);

            // ȭ��ǥ ����
            eventDirectionArrow.CreateArrow(randomEvent);

            // ��ǳ�� ����
            speechBubble.PlaySpeechBubble();
        }
    }

    // ���� ���߻�Ȳ ��ȣ�ۿ� ���� ����
    private void OnRandomEventSuccess(RandomEventObject successEvent)
    {
        Debug.Log("�̺�Ʈ ��ȣ�ۿ� ���� ���� : " + successEvent.name);

        createdEventList.Remove(successEvent);
        eventDirectionArrow.RemoveArrow(successEvent);
        Destroy(successEvent.gameObject);
    }

    // ���� ���߻�Ȳ ��ȣ�ۿ� ���� ����
    private void OnRandomEventInteractFailed(RandomEventObject failedEvent)
    {
        Debug.Log("�̺�Ʈ ��ȣ�ۿ� ���� ���� : " + failedEvent.name);

        createdEventList.Remove(failedEvent);
        eventDirectionArrow.RemoveArrow(failedEvent);
        Destroy(failedEvent.gameObject);
    }
}
