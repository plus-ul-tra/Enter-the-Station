using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [SerializeField]
    private List<Task> taskList;
    //private KindOfTask kindOfTask;
    private Task currentTask; //���� �������� Task
    
    //private int currentTaskIndex = 0;

    public void StartTask(KindOfTask task) //�긦 ��� ȣ���� ����� enum �Ķ���Ϳ� �Բ�
    { //���⼭ enum���� Task ��û ����
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
                return true; // �۾� ���� ���� �ִٸ� ture
            }
        }
        return false; //�۾� ���� ����
    }
}
