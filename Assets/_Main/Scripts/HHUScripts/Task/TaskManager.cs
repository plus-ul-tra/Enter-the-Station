using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private List<Task> taskList;
    //private KindOfTask kindOfTask;
    private Task currentTask; //현재 진행중인 Task
    
    //private int currentTaskIndex = 0;

    public void StartTask(KindOfTask task) //얘를 어디서 호출해 줘야함 enum 파라미터와 함께
    { //여기서 enum으로 Task 요청 받음
        if (!CheckOnTask())
        {
            for (int i = 0; i < taskList.Count; i++)
            {
                if (taskList[i].kindOfTask == task)
                {
                    taskList[i].Open();
                    break;
                }
            }
        }
    }

    private bool CheckOnTask()
    {
         for(int i=0; i < taskList.Count; i++)
        {
            if (taskList[i].isOnTask)
            {
                return true; // 작업 중인 것이 있다면 ture
            }
        }
        return false; //작업 없는 상태
    }
}
