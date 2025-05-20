using UnityEngine;
using System.Collections.Generic;

public class MapGuide : Task
{
    public Way way; // 이어줄 길
    private Way selectStartPoint;
    private List<Node> passedNodes = new List<Node>();
    private List<Way> ways = new List<Way>();
    private Node lastConnectedNode = null;
    [SerializeField]
    private List<Node> goals; // 마지막 노드들
    private Node goal; // 랜덤으로 정해질 목표

    private void OnEnable()
    {
        InitGame();
    }

    private Way CreateNewWay(Node node)
    {
        SoundManager.Instance.PlaySFX("Map_line_input");
        RectTransform nodeRect = node.GetComponent<RectTransform>();
        Way newSP = Instantiate(way, nodeRect.parent);
        RectTransform newRect = newSP.GetComponent<RectTransform>();
        newRect.anchoredPosition = nodeRect.anchoredPosition;
        newRect.localScale = Vector3.one; // 스케일 보정 (중요)
        return newSP;
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limitTime)
        {
            stageManager.DecreasePlayerHp();
            failedImage.SetActive(true);
            Close();
            timer = 0.0f;
            foreach (var way in ways)
            {
                //선 파괴
                if (way != null)
                {
                    Destroy(way.gameObject);

                }
            }
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit2D hit = Physics2D.Raycast(Input.mousePosition, Vector2.right, 1.0f);
            if (hit.collider != null)
            {
                var point = hit.collider.GetComponentInParent<StartNode>();
                if (point != null)
                {
                    selectStartPoint = CreateNewWay(point);
                    ways.Add(selectStartPoint);
                    passedNodes.Add(point);
                    lastConnectedNode = point;      
                }
            }
            
        }
        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Debug.Break(); // 일시 정지 (Editor에서만 작동)
        //}
        if (Input.GetMouseButton(0) && selectStartPoint != null)
        {
            RaycastHit2D[] hits = Physics2D.RaycastAll(Input.mousePosition, Vector2.right, 1.0f); //tracking mouse pos
            foreach (var hit in hits)
            {
                if (hit.collider != null) //충돌시
                {
                    var node = hit.collider.GetComponentInParent<Node>();
                    if (node != null && ! passedNodes.Contains(node) && lastConnectedNode.CanConnectTo(node))
                    {
                      
                        
                        selectStartPoint.ConnectNode(node);
                        //node.ConnectWire(selectStartPoint);
                        passedNodes.Add(node);
                        lastConnectedNode = node;
                        selectStartPoint = CreateNewWay(node);
                        ways.Add(selectStartPoint);
                        break;
                    }
                }
            }

        }
        
        if (Input.GetMouseButtonUp(0))
        {//마우스뗌
            foreach (var way in ways)
            {
                //선 파괴
                if (way != null)
                {
                    Destroy(way.gameObject);
                    
                }
            }

            // 리스트 초기화
            passedNodes.Clear();
            ways.Clear();
            selectStartPoint = null;
            lastConnectedNode = null;
        }

        if (selectStartPoint != null)
        {
            selectStartPoint.SetTarget(Input.mousePosition, -8.0f);
        }
        CheckComplete();
    }

    public override void InitGame()
    {
        SoundManager.Instance.PlaySFX("Map_open");
        successImage.SetActive(false);
        failedImage.SetActive(false);
        timer = 0.0f;
        for (int i = 0; i < goals.Count; i++)
        {
            goals[i].gameObject.SetActive(true);
        }
        passedNodes.Clear();
        ways.Clear();
        selectStartPoint = null;
        lastConnectedNode = null;

        int randIndex = Random.Range(0, goals.Count); 
        goal = goals[randIndex];
        for (int i = 0; i < goals.Count; i++)
        {
            if (i == randIndex) continue;
            goals[i].gameObject.SetActive(false);
        }
        // 나머지는 비활성화
    }

    private void CheckComplete()
    {
        bool completeCheck = false;
        if(goal == lastConnectedNode)
        {
            completeCheck = true;
           
        }
      
        if (completeCheck)
        {
            timer = 0.0f;
            successImage.SetActive(true);
            SoundManager.Instance.PlaySFX("Map_finish");
            Close();
            foreach (var way in ways)
            {
                //선 파괴
                if (way != null)
                {
                    Destroy(way.gameObject);

                }
            }
            
        }

    }

    
}
