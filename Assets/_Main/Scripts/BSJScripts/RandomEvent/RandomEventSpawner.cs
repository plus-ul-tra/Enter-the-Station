using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class RandomEventSpawner : MonoBehaviour
{
    [Header("�Ŵ���")]
    public StageManager stageManager;                   // �������� �Ŵ���
    public TaskManager taskManager;                     // �̴ϰ��� ���� �Ŵ���

    [Header("ȭ��ǥ, ������")]
    public EventDirectionArrow eventDirectionArrow;     // �̺�Ʈ ȭ��ǥ
    public SpeechBubble speechBubble;                   // ������ ��ǳ��

    [Header("���߻�Ȳ ������Ʈ")]
    public List<RandomEventObject> randomEventList;     // ���� ���߻�Ȳ �������� ���� ����Ʈ
    public List<RandomEventObject> createdEventList;    // ������ ���߻�Ȳ ���� ����Ʈ

    [Header("���߻�Ȳ �߻� �ֱ�(�ð�)")]
    [SerializeField]
    private float eventSpawnTime = 15f;
    private float currentSpawnTimer = 0f;               // ���� ���� �ð�

    // --------------------------------------------------

    [Header("zone ���� ����Ʈ ��")]
    public List<Transform> zoneSpawnPoint_01;
    public List<Transform> zoneSpawnPoint_02;
    public List<Transform> zoneSpawnPoint_03;
    public List<Transform> zoneSpawnPoint_04;
    public List<Transform> zoneSpawnPoint_05;
    public List<Transform> zoneSpawnPoint_06;

    // zone ���� ����Ʈ�� ����
    private int[,] randomSequences = new int[,]
    {
        {1, 3, 5, 2, 4, 6},  // zone ����1
        {1, 3, 6, 4, 2, 5},  // zone ����2
        {1, 4, 2, 5, 3, 6},  // zone ����3
        {1, 4, 6, 3, 5, 2},  // zone ����4
        {2, 5, 3, 1, 4, 6},  // zone ����5
        {2, 5, 3, 6, 4, 1},  // zone ����6
        {3, 1, 5, 2, 6, 4},  // zone ����7
        {3, 5, 2, 6, 4, 1},  // zone ����8
    };

    private int currentSequenceIndex = 0;       // ���� ���õ� zone ������ �ε���
    private int currentSequenceElement = 0;     // ���� ���� ������ �� ��° ��ġ����
    
    private struct SpawnPointData
    {
        public int zoneIndex;
        public int spawnPoint;
    }
    private SpawnPointData spawnPointData;       // ���� ���� �� ��° ���� ����Ʈ ����?

    private void Start()
    {
        // TODO : ������ �ƽ� ������ �� ���Ŀ� �����ϵ��� ���� (10�ʿ��� �׳� �״�� �����ص���)
        SelectRandomEventZone();
    }

    private void Update()
    {
        // ���߻�Ȳ �߻� �ֱ� ���� �̺�Ʈ ����
        currentSpawnTimer += Time.deltaTime;

        if (currentSpawnTimer >= eventSpawnTime)
        {
            currentSpawnTimer -= eventSpawnTime;

            // ���� �������� ��ġ ��������
            int zoneNumber = randomSequences[currentSequenceIndex, currentSequenceElement];
            spawnPointData.zoneIndex = zoneNumber;

            // zoneNumber ������� ��ġ ���ϱ� (��: zoneNumber�� ���� �̸� ���ǵ� ��ġ ���)
            Transform selectSpawnPoint;
            switch (zoneNumber)
            {
                case 1: // zone 1
                    spawnPointData.spawnPoint = Random.Range(0, zoneSpawnPoint_01.Count);
                    selectSpawnPoint = zoneSpawnPoint_01[spawnPointData.spawnPoint];
                    break;
                case 2: // zone 2
                    spawnPointData.spawnPoint = Random.Range(0, zoneSpawnPoint_02.Count);
                    selectSpawnPoint = zoneSpawnPoint_02[spawnPointData.spawnPoint];
                    break;
                case 3: // zone 3
                    spawnPointData.spawnPoint = Random.Range(0, zoneSpawnPoint_03.Count);
                    selectSpawnPoint = zoneSpawnPoint_03[spawnPointData.spawnPoint];
                    break;
                case 4: // zone 4
                    spawnPointData.spawnPoint = Random.Range(0, zoneSpawnPoint_04.Count);
                    selectSpawnPoint = zoneSpawnPoint_04[spawnPointData.spawnPoint];
                    break;
                case 5: // zone 5
                    spawnPointData.spawnPoint = Random.Range(0, zoneSpawnPoint_05.Count);
                    selectSpawnPoint = zoneSpawnPoint_05[spawnPointData.spawnPoint];
                    break;
                case 6: // zone 6
                    spawnPointData.spawnPoint = Random.Range(0, zoneSpawnPoint_06.Count);
                    selectSpawnPoint = zoneSpawnPoint_06[spawnPointData.spawnPoint];
                    break;
                default:
                    spawnPointData.spawnPoint = Random.Range(0, zoneSpawnPoint_01.Count);
                    selectSpawnPoint = zoneSpawnPoint_01[spawnPointData.spawnPoint];
                    break;
            }

            // ���� �̺�Ʈ ������Ʈ ����
            CreateRandomEventObject(selectSpawnPoint.position);

            // ���� step���� �̵�
            currentSequenceElement++;

            // ���� ������ �� ���� �ٽ� ���� ���� ����
            if (currentSequenceElement >= randomSequences.GetLength(1))
            {
                SelectRandomEventZone();
            }
        }
    }

    /// <summary>
    /// ���� ������Ʈ�� �����ϴ� �Լ�
    /// </summary>
    /// <param name="eventPosition"></param>
    public void CreateRandomEventObject(Vector3 eventPosition)
    {
        if (stageManager.playerCurHp <= 0) return;

        // ���� �̺�Ʈ �����ϱ�
        int randomIndex = SelectRandomEventIndex() - 1;

        // ����� ����
        Debug.Log($"���� ���� : {currentSequenceIndex}");
        Debug.Log($"���� ������ �ε��� : {currentSequenceElement}");
        Debug.Log($"���� �� : {spawnPointData.zoneIndex}");
        Debug.Log($"���� ���� ��������Ʈ : {spawnPointData.spawnPoint}");
        Debug.Log($"���� �̺�Ʈ ��ȣ: {randomIndex + 1}");


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
            CreateSpeech(randomEvent);
        }
    }

    // ��ǳ�� ����
    private void CreateSpeech(RandomEventObject randomEvent)
    {
        if (randomEvent.task == KindOfTask.FixWire)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.ELV);
        }
        else if (randomEvent.task == KindOfTask.ArrowMatch)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.SLEEP);
        }
        else if (randomEvent.task == KindOfTask.MaintainingGauge)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.HEART);
        }
        else if (randomEvent.task == KindOfTask.MovingCircle)
        {

        }
    }

    // ���� ���߻�Ȳ ��ȣ�ۿ� ���� ����
    private void OnRandomEventSuccess(RandomEventObject successEvent)
    {
        Debug.Log("�̺�Ʈ ��ȣ�ۿ� ���� ���� : " + successEvent.name);

        // �̺�Ʈ ���� (���� ����)
        successEvent.onEventSuccess -= OnRandomEventSuccess;
        successEvent.onEventFailed -= OnRandomEventInteractFailed;

        createdEventList.Remove(successEvent);
        eventDirectionArrow.RemoveArrow(successEvent);
        Destroy(successEvent.gameObject);
    }

    // ���� ���߻�Ȳ ��ȣ�ۿ� ���� ����
    private void OnRandomEventInteractFailed(RandomEventObject failedEvent)
    {
        Debug.Log("�̺�Ʈ ��ȣ�ۿ� ���� ���� : " + failedEvent.name);

        // �̺�Ʈ ���� (���� ����)
        failedEvent.onEventSuccess -= OnRandomEventSuccess;
        failedEvent.onEventFailed -= OnRandomEventInteractFailed;

        stageManager.DecreasePlayerHp();                // �÷��̾� Hp ����
        createdEventList.Remove(failedEvent);           // ������ ���߻�Ȳ ������Ʈ ����
        eventDirectionArrow.RemoveArrow(failedEvent);   // �����ϴ� ȭ��ǥ ����
        Destroy(failedEvent.gameObject);                // ������Ʈ �ı�
    }

    // ���� �̺�Ʈ ���� ���� �����Ѵ�.
    private void SelectRandomEventZone()
    {
        // ���� ���� �� �ϳ� ����
        currentSequenceIndex = Random.Range(0, randomSequences.GetLength(0));
        currentSequenceElement = 0; // ������ ó������ ����
    }

    // ���� �̺�Ʈ�� �ε����� �����Ѵ�.
    private int SelectRandomEventIndex()
    {
        int eventIndex = 0;

        if(spawnPointData.zoneIndex == 1 && spawnPointData.spawnPoint == 0)
        {
            eventIndex = 7;
        }
        else if (spawnPointData.zoneIndex == 1 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 3);

            switch(randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 1 && spawnPointData.spawnPoint == 2)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 3;
                    break;
                case 2:
                    eventIndex = 6;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 1 && spawnPointData.spawnPoint == 3)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 5;
                    break;
                case 1:
                    eventIndex = 6;
                    break;
                case 2:
                    eventIndex = 9;
                    break;
                default:
                    eventIndex = 5;
                    break;
            }
        }

        else if (spawnPointData.zoneIndex == 2 && spawnPointData.spawnPoint == 0)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 6;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 2 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 5;
                    break;
                case 1:
                    eventIndex = 6;
                    break;
                default:
                    eventIndex = 5;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 2 && spawnPointData.spawnPoint == 2)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 9;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 2 && spawnPointData.spawnPoint == 3)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 6;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }

        else if (spawnPointData.zoneIndex == 3 && spawnPointData.spawnPoint == 0)
        {
            eventIndex = 4;
        }
        else if (spawnPointData.zoneIndex == 3 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 5;
                    break;
                case 1:
                    eventIndex = 6;
                    break;
                default:
                    eventIndex = 5;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 3 && spawnPointData.spawnPoint == 2)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 6;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }

        else if (spawnPointData.zoneIndex == 4 && spawnPointData.spawnPoint == 0)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 4 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 9;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 4 && spawnPointData.spawnPoint == 2)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 4 && spawnPointData.spawnPoint == 3)
        {
            eventIndex = 1;
        }

        else if (spawnPointData.zoneIndex == 5 && spawnPointData.spawnPoint == 0)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 5 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 9;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 5 && spawnPointData.spawnPoint == 2)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 5 && spawnPointData.spawnPoint == 3)
        {
            eventIndex = 1;
        }

        else if (spawnPointData.zoneIndex == 6 && spawnPointData.spawnPoint == 0)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 6 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 3;
                    break;
                case 1:
                    eventIndex = 9;
                    break;
                default:
                    eventIndex = 3;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 6 && spawnPointData.spawnPoint == 2)
        {
            eventIndex = 1;
        }

        return eventIndex;
    }
}
