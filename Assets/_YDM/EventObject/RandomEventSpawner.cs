using UnityEngine;
using System.Collections.Generic;
using System.Threading;

public class RandomEventSpawner : MonoBehaviour
{
    public TaskManager taskManager;                     // 미니게임 실행 매니저
    public EventDirectionArrow eventDirectionArrow;     // 이벤트 화살표
    public SpeechBubble speechBubble;                   // 무전기 말풍선

    public List<RandomEventObject> randomEventList;     // 랜덤 돌발상황 업스케일 관리 리스트
    public List<RandomEventObject> createdEventList;    // 생성된 돌발상황 관리 리스트

    [Header("돌발상황 발생 주기(시간)")]
    [SerializeField]
    private float eventSpawnTime = 15f;
    private float currentSpawnTimer = 0f;               // 현재 스폰 시간

    private void Start()
    {
        // 테스트 : 시작할 때 몇 초 후에 돌발상황 발생할 것인지?
        currentSpawnTimer = 10f;
    }

    private void Update()
    {
        // 돌발상황 발생 주기 마다 이벤트 생성
        currentSpawnTimer += Time.deltaTime;

        if(currentSpawnTimer >= eventSpawnTime)
        {
            currentSpawnTimer -= eventSpawnTime;

            // TODO : 테스트용 랜덤 스폰 나중에 변경해야함.
            Vector3 randomPosition = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), 0);
            CreateRandomEventObject(randomPosition);
        }
    }

    /// <summary>
    /// 랜덤 오브젝트를 생성하는 함수
    /// </summary>
    /// <param name="eventPosition"></param>
    public void CreateRandomEventObject(Vector3 eventPosition)
    {
        int randomIndex = Random.Range(0, randomEventList.Count);

        GameObject eventObject = Instantiate(randomEventList[randomIndex].gameObject, eventPosition, Quaternion.identity);
        if(eventObject.TryGetComponent<RandomEventObject>(out RandomEventObject randomEvent))
        {
            createdEventList.Add(randomEvent);

            // 콜백 (상호작용 성공 / 실패) 등록
            randomEvent.onEventSuccess += OnRandomEventSuccess;
            randomEvent.onEventFailed += OnRandomEventInteractFailed;

            // 미니게임 생성용 참조
            randomEvent.ReferTaskManager(taskManager);

            // 화살표 생성
            eventDirectionArrow.CreateArrow(randomEvent);

            // 말풍선 생성
            speechBubble.PlaySpeechBubble();
        }
    }

    // 랜덤 돌발상황 상호작용 성공 감지
    private void OnRandomEventSuccess(RandomEventObject successEvent)
    {
        Debug.Log("이벤트 상호작용 성공 감지 : " + successEvent.name);

        createdEventList.Remove(successEvent);
        eventDirectionArrow.RemoveArrow(successEvent);
        Destroy(successEvent.gameObject);
    }

    // 랜덤 돌발상황 상호작용 실패 감지
    private void OnRandomEventInteractFailed(RandomEventObject failedEvent)
    {
        Debug.Log("이벤트 상호작용 실패 감지 : " + failedEvent.name);

        createdEventList.Remove(failedEvent);
        eventDirectionArrow.RemoveArrow(failedEvent);
        Destroy(failedEvent.gameObject);
    }
}
