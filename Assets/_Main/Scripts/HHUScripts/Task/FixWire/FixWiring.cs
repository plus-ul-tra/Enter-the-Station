using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public enum EWireColor
{
    None = -1,
    Red,
    Blue,
    Yellow,
    Magenta,
}
public class FixWiring : Task
{
    [SerializeField]
    private List<LeftWire> leftWires;
    [SerializeField]
    private List<RightWire> rightWires;
    private LeftWire selectWire;
    private bool isOver = false;
    private bool isDone = false; 

    private void OnEnable()
    {
        InitGame();

        List<int> numberPool = new List<int>();
        for(int i = 0; i < 4; i++)
        {
            numberPool.Add(i);
        }
        int index = 0;
        while(numberPool.Count != 0)
        {
            var number = numberPool[Random.Range(0, numberPool.Count)];
            leftWires[index++].SetWireColor((EWireColor)number);
            numberPool.Remove(number);
        }
        for (int i = 0; i < 4; i++)
        {
            numberPool.Add(i);
        }

        index = 0;
        while (numberPool.Count != 0)
        {
            var number = numberPool[Random.Range(0, numberPool.Count)];
            rightWires[index++].SetWireColor((EWireColor)number);
            numberPool.Remove(number);
        }
        SoundManager.Instance.PlaySFX("Fix_elevator");
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limitTime&&!isDone)
        {
            if(stageManager != null)
                stageManager.DecreasePlayerHp(); //singleton 객체 사용

            failedImage.SetActive(true);
            SoundManager.Instance.PlaySFX("Fail_sound");
            Close();
            timer = 0.0f;
            isDone = true;
            return;
        }

        if (Input.GetMouseButtonDown(0)&&!isOver) //down 되어있는 동안은 선을 끌고 있다는 것
        {

            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1.0f);
            if (hit.collider != null)
            {
                //Debug.Log("clicked!");
                var left = hit.collider.GetComponentInParent<LeftWire>();
                if (left != null)
                {
                    selectWire = left;
                }
            }
        }
        if (Input.GetMouseButtonUp(0)&&!isOver)
        {
            if (selectWire != null)
            {
                RaycastHit2D[] hits = Physics2D.RaycastAll(Input.mousePosition, Vector2.right, 1.0f);
                foreach (var hit in hits)
                {
                    if (hit.collider != null)
                    {
                        var right = hit.collider.GetComponentInParent<RightWire>();
                        if (right != null)
                        {
                            selectWire.SetTarget(hit.transform.position, -100.0f);
                            selectWire.ConnectWire(right);
                            right.ConnectWire(selectWire);
                            selectWire = null;
                            CheckComplete();
                            return;
                        }
                    }
                }


                selectWire.ResetTarget();
                selectWire.DisconnectWire();
                selectWire = null;
                CheckComplete();
            }
        }

        if (selectWire != null)
        {
            selectWire.SetTarget(Input.mousePosition, -100.0f);
        }
    }

    private void CheckComplete()
    {  //Could close when all Wires are Connected;
        bool isAllComplete = true; //전선 체크, 
        foreach(var wire in leftWires)
        {
            if (!wire.isConnected)
            {
                isAllComplete = false;
                break;
            }
        }
        if (isAllComplete && !isDone)
        {
            isDone = true;
            timer = 0.0f;
            // 성공
            successImage.SetActive(true);
            CountManager.Instance.AddClearCount();
            SoundManager.Instance.PlaySFX("Fix_elevator_finish");
            Close();
            
        }
    }

    public override void InitGame()
    {
        isDone = false;
        timer = 0.0f;
        isOver = false;
        successImage.SetActive(false);
        failedImage.SetActive(false);
        for (int i = 0; i < leftWires.Count; i++)
        {
            leftWires[i].ResetTarget();
            leftWires[i].DisconnectWire();
        }
    }
}
