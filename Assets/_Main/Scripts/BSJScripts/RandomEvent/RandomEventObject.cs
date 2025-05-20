using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class RandomEventObject : MonoBehaviour
{
    public KindOfTask task;

    [Header("��ȣ�ۿ� ���ϸ� ���еǴ� �ð�")]
    [SerializeField]
    private float failTime = 15f;

    [Header("Ʃ�丮���̸� ���е��� ����")]
    [SerializeField] private bool isTutorial = false;

    public Action<RandomEventObject> onEventFailed;   // ���� �� ȣ��� �ݹ�
    public Action<RandomEventObject> onEventSuccess;  // ���� �� ȣ��� �ݹ�

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

        Debug.Log("�̺�Ʈ ����");
        onEventFailed?.Invoke(this);
    }

    /// <summary>
    /// �̺�Ʈ ��ȣ�ۿ� �������� �� ����
    /// </summary>
    public void CompleteInteractEvent()
    {
        if (isComplete) return;

        isComplete = true;
        Debug.Log("�̺�Ʈ ����");
        onEventSuccess?.Invoke(this);

        // �̴ϰ��� ����
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
