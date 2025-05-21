using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class RandomEventSpawner : MonoBehaviour
{
    [Header("매니저")]
    public StageManager stageManager;                   // 스테이지 매니저
    public TaskManager taskManager;                     // 미니게임 실행 매니저

    [Header("화살표, 무전기")]
    public EventDirectionArrow eventDirectionArrow;     // 이벤트 화살표
    public SpeechBubble speechBubble;                   // 무전기 말풍선

    [Header("돌발상황 오브젝트")]
    public List<RandomEventObject> randomEventList;     // 랜덤 돌발상황 업스케일 관리 리스트
    public List<RandomEventObject> createdEventList;    // 생성된 돌발상황 관리 리스트

    [Header("돌발상황 발생 주기(시간)")]
    [SerializeField]
    private float eventSpawnTime = 15f;
    private float currentSpawnTimer = 0f;               // 현재 스폰 시간

    // --------------------------------------------------

    [Header("zone 스폰 포인트 들")]
    public List<Transform> zoneSpawnPoint_01;
    public List<Transform> zoneSpawnPoint_02;
    public List<Transform> zoneSpawnPoint_03;
    public List<Transform> zoneSpawnPoint_04;
    public List<Transform> zoneSpawnPoint_05;
    public List<Transform> zoneSpawnPoint_06;

    // zone 수열 리스트로 정의
    private int[,] randomSequences = new int[,]
    {
        {1, 3, 5, 2, 4, 6},  // zone 수열1
        {1, 3, 6, 4, 2, 5},  // zone 수열2
        {1, 4, 2, 5, 3, 6},  // zone 수열3
        {1, 4, 6, 3, 5, 2},  // zone 수열4
        {2, 5, 3, 1, 4, 6},  // zone 수열5
        {2, 5, 3, 6, 4, 1},  // zone 수열6
        {3, 1, 5, 2, 6, 4},  // zone 수열7
        {3, 5, 2, 6, 4, 1},  // zone 수열8
    };

    private int currentSequenceIndex = 0;       // 현재 선택된 zone 수열의 인덱스
    private int currentSequenceElement = 0;     // 현재 수열 내에서 몇 번째 위치인지

    // --------------------------------------------------
    [Header("튜토리얼이면 체크")]
    [SerializeField] private bool isTutorial = false;

    // 현재 씬 체크 (1일차, 2일차, 3일차)
    Scene currentScene;

    private struct SpawnPointData
    {
        public int zoneIndex;
        public int spawnPoint;
    }
    private SpawnPointData spawnPointData;       // 현재 존의 몇 번째 스폰 포인트 인지?

    private void Start()
    {
        // 현재 씬 체크
        currentScene = SceneManager.GetActiveScene();

        // TODO : 튜토리얼 일때에는 그냥 실행, 
        if (isTutorial)
        {
            SelectRandomEventZone();
        }

        // 일차일 경우에는 시작 시간 10초에서부터 시작
        else
        {
            currentSpawnTimer = 10f;
            SelectRandomEventZone();
        }
    }

    private void Update()
    {
        // 돌발상황 발생 주기 마다 이벤트 생성
        currentSpawnTimer += Time.deltaTime;

        if (currentSpawnTimer >= eventSpawnTime)
        {
            currentSpawnTimer -= eventSpawnTime;

            // 현재 수열에서 위치 가져오기
            int zoneNumber = randomSequences[currentSequenceIndex, currentSequenceElement];
            spawnPointData.zoneIndex = zoneNumber;

            // zoneNumber 기반으로 위치 정하기 (예: zoneNumber에 따라 미리 정의된 위치 사용)
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

            // 랜덤 이벤트 오브젝트 생성
            CreateRandomEventObject(selectSpawnPoint.position);

            // 다음 step으로 이동
            currentSequenceElement++;

            // 수열 끝까지 다 돌면 다시 랜덤 수열 선택
            if (currentSequenceElement >= randomSequences.GetLength(1))
            {
                SelectRandomEventZone();
            }
        }
    }

    /// <summary>
    /// 랜덤 오브젝트를 생성하는 함수
    /// </summary>
    /// <param name="eventPosition"></param>
    public void CreateRandomEventObject(Vector3 eventPosition)
    {
        if (stageManager.playerCurHp <= 0) return;

        // 랜덤 이벤트 선택하기
        int randomIndex = SelectRandomEventIndex() - 1;

        // 디버그 찍어보기
        Debug.Log($"현재 수열 : {currentSequenceIndex}");
        Debug.Log($"현재 수열의 인덱스 : {currentSequenceElement}");
        Debug.Log($"현재 존 : {spawnPointData.zoneIndex}");
        Debug.Log($"현재 존의 스폰포인트 : {spawnPointData.spawnPoint}");
        Debug.Log($"현재 이벤트 번호: {randomIndex + 1}");


        GameObject eventObject = Instantiate(randomEventList[randomIndex].gameObject, eventPosition, Quaternion.identity);
        if (eventObject.TryGetComponent<RandomEventObject>(out RandomEventObject randomEvent))
        {
            createdEventList.Add(randomEvent);

            // 콜백 (상호작용 성공 / 실패) 등록
            randomEvent.onEventSuccess += OnRandomEventSuccess;
            randomEvent.onEventFailed += OnRandomEventInteractFailed;

            // 미니게임 생성용 참조
            randomEvent.ReferTaskManager(taskManager);

            // 화살표 생성
            eventDirectionArrow.CreateArrow(randomEvent, spawnPointData.zoneIndex);

            // 말풍선 생성
            CreateSpeech(randomEvent);
        }
    }

    // 말풍선 생성
    private void CreateSpeech(RandomEventObject randomEvent)
    {
        if (randomEvent.task == KindOfTask.FixWire)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.ELV, spawnPointData.zoneIndex);
        }
        else if (randomEvent.task == KindOfTask.ArrowMatch)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.SLEEP, spawnPointData.zoneIndex);
        }
        else if (randomEvent.task == KindOfTask.MaintainingGauge)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.HEART, spawnPointData.zoneIndex);
        }
        else if (randomEvent.task == KindOfTask.MovingCircle)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.CS, spawnPointData.zoneIndex);
        }
        else if (randomEvent.task == KindOfTask.Swinging)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.CSSLEEP, spawnPointData.zoneIndex);
        }
        else if (randomEvent.task == KindOfTask.MapGuide)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.MAP, spawnPointData.zoneIndex);
        }
        else if (randomEvent.task == KindOfTask.StackingGauge)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.FALL, spawnPointData.zoneIndex);
        }
        else if (randomEvent.task == KindOfTask.TurningKey)
        {
            speechBubble.PlaySpeechBubble(SpeechKey.ESC, spawnPointData.zoneIndex);
        }
    }

    // 랜덤 돌발상황 상호작용 성공 감지
    private void OnRandomEventSuccess(RandomEventObject successEvent)
    {
        Debug.Log("이벤트 상호작용 성공 감지 : " + successEvent.name);

        // 이벤트 해제 (누수 방지)
        successEvent.onEventSuccess -= OnRandomEventSuccess;
        successEvent.onEventFailed -= OnRandomEventInteractFailed;

        createdEventList.Remove(successEvent);
        eventDirectionArrow.RemoveArrow(successEvent);
        Destroy(successEvent.gameObject);
    }

    // 랜덤 돌발상황 상호작용 실패 감지
    private void OnRandomEventInteractFailed(RandomEventObject failedEvent)
    {
        Debug.Log("이벤트 상호작용 실패 감지 : " + failedEvent.name);

        // 이벤트 해제 (누수 방지)
        failedEvent.onEventSuccess -= OnRandomEventSuccess;
        failedEvent.onEventFailed -= OnRandomEventInteractFailed;

        stageManager.DecreasePlayerHp();                // 플레이어 Hp 감소
        createdEventList.Remove(failedEvent);           // 생성된 돌발상황 오브젝트 삭제
        eventDirectionArrow.RemoveArrow(failedEvent);   // 추적하는 화살표 삭제
        Destroy(failedEvent.gameObject);                // 오브젝트 파괴
    }

    // 랜덤 이벤트 생성 존을 선택한다.
    private void SelectRandomEventZone()
    {
        // 랜덤 수열 중 하나 선택
        currentSequenceIndex = Random.Range(0, randomSequences.GetLength(0));
        currentSequenceElement = 0; // 수열의 처음부터 시작
    }

    // 랜덤 이벤트의 인덱스를 선택한다.
    private int SelectRandomEventIndex()
    {
        int eventIndex = 0;

        if (spawnPointData.zoneIndex == 1 && spawnPointData.spawnPoint == 0)
        {
            eventIndex = 6;
        }
        else if (spawnPointData.zoneIndex == 1 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 7;
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
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 5;
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
                    eventIndex = 4;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 4;
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
                    eventIndex = 5;
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
                    eventIndex = 4;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                default:
                    eventIndex = 4;
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
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 8;
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
                    eventIndex = 5;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }

        else if (spawnPointData.zoneIndex == 3 && spawnPointData.spawnPoint == 0)
        {
            eventIndex = 3;
        }
        else if (spawnPointData.zoneIndex == 3 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 4;
                    break;
                case 1:
                    eventIndex = 5;
                    break;
                default:
                    eventIndex = 4;
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
                    eventIndex = 5;
                    break;
                case 2:
                    eventIndex = 7;
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
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 4 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 4 && spawnPointData.spawnPoint == 2)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 7;
                    break;
                default:
                    eventIndex = 2;
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
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 5 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 5 && spawnPointData.spawnPoint == 2)
        {
            int randValue = Random.Range(0, 2);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 7;
                    break;
                default:
                    eventIndex = 2;
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
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 7;
                    break;
                default:
                    eventIndex = 2;
                    break;
            }
        }
        else if (spawnPointData.zoneIndex == 6 && spawnPointData.spawnPoint == 1)
        {
            int randValue = Random.Range(0, 3);

            switch (randValue)
            {
                case 0:
                    eventIndex = 2;
                    break;
                case 1:
                    eventIndex = 4;
                    break;
                case 2:
                    eventIndex = 8;
                    break;
                default:
                    eventIndex = 2;
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
