using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RandomEventObject : MonoBehaviour
{
    public KindOfTask task;

    [Header("상호작용 안하면 실패되는 시간")]
    [SerializeField]
    private float failTime = 15f;

    [Header("튜토리얼이면 실패되지 않음")]
    [SerializeField] private bool isTutorial = false;

    public Action<RandomEventObject> onEventFailed;   // 실패 시 호출될 콜백
    public Action<RandomEventObject> onEventSuccess;  // 성공 시 호출될 콜백

    private bool isComplete = false;
    private Coroutine failCoroutine;

    private GameObject interactUI;

    [HideInInspector] public TaskManager taskManager;
    private void Awake()
    {
        interactUI = transform.GetChild(0).gameObject;
        interactUI.SetActive(false);
    }

    private void Start()
    {
        if(!isTutorial)
        {
            StartCoroutine(FailInteractEvent());
        }
        else { /*DoNothing*/ }
    }

    IEnumerator FailInteractEvent()
    {
        yield return new WaitForSeconds(failTime);

        if (isComplete) yield break;

        Debug.Log("이벤트 실패");
        onEventFailed?.Invoke(this);
    }

    /// <summary>
    /// 이벤트 상호작용 성공했을 때 실행
    /// </summary>
    public void CompleteInteractEvent()
    {
        if (isComplete) return;

        isComplete = true;
        Debug.Log("이벤트 성공");
        onEventSuccess?.Invoke(this);

        // 미니게임 실행
        taskManager.StartTask(task);
    }

    public void ReferTaskManager(TaskManager _taskManager)
    {
        taskManager = _taskManager;
    }

    private void OnDisable()
    {
        if(failCoroutine != null)
        {
            StopCoroutine(failCoroutine);
            failCoroutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            if(interactUI != null)
            {
                interactUI.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (interactUI != null)
            {
                interactUI.SetActive(false);
            }
        }
    }
}
