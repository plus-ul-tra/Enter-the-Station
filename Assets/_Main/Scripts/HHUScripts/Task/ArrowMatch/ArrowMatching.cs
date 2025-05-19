using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ArrowDir
{
    Up,
    Right,
    Down,
    Left,
}
public class ArrowMatching : Task
{   
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private GameObject blockPrefabs;
    [SerializeField]
    private int maxBlockCount = 9;
    [SerializeField]
    private int maxSuccessCount = 3; // task success Complete
    private int successCount = 0; //9�� ���� ���߸� count++
    private List<ArrowButton> arrowBlocks = new List<ArrowButton>();
    private int matchIndex =0;
    private bool isOver = false;

    private void OnEnable()
    {
        ClearBlocks();
        InitGame();
        successCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= limitTime)
        {
            failedImage.SetActive(true);
            Close();
            isOver = true;
            stageManager.DecreasePlayerHp();
            timer = 0.0f;
            return;
        }

        if (arrowBlocks.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.UpArrow)&&!isOver) TryMatch(ArrowDir.Up);
        else if (Input.GetKeyDown(KeyCode.RightArrow)&&!isOver) TryMatch(ArrowDir.Right);
        else if (Input.GetKeyDown(KeyCode.DownArrow)&&!isOver) TryMatch(ArrowDir.Down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)&&!isOver) TryMatch(ArrowDir.Left);
        checkSuccess();

        if (successCount == maxSuccessCount && timer < limitTime) 
        {
            // task ����
            //�ʿ�� �Լ� �߰�
            successImage.SetActive(true);
            timer = 0.0f;
            isOver = true;
            Close();
            return;
        }
    }

    private void SetBlock()
    {
       // arrowBlocks.Clear();
        matchIndex = 0;
        for (int i=0; i< maxBlockCount; i++)
        {
            //���� ���� ����� �ƴѰ� ������
            int random = Random.Range(0, 4);
            GameObject obj = Instantiate(blockPrefabs, container.transform);

            ArrowButton arrow = obj.GetComponent<ArrowButton>();

            arrow.SetButtonDir((ArrowDir)random);
            arrowBlocks.Add(arrow);
        }
        
    }
    private void ClearBlocks()
    {
        foreach (var block in arrowBlocks)
        {
            if (block != null)
                Destroy(block.gameObject);
        }
        arrowBlocks.Clear();
    }
    private void checkSuccess()
    {
        if(matchIndex == maxBlockCount)
        {
            successCount++;
        }
        if(matchIndex == maxBlockCount && successCount < maxSuccessCount)
        {
            ClearBlocks();
            SetBlock();
        }
    }
    private void TryMatch(ArrowDir inputDir)
    {   //����ڵ�
        if (arrowBlocks.Count == 0) return;
        if(matchIndex>= maxBlockCount) return;

        ArrowButton Block = arrowBlocks[matchIndex];
        if(Block.Dir == inputDir)
        {
            Block.Matched();
            matchIndex++;
        }
        else
        {
            //Debug.Log("����");
            //Block.MatcheFail();

        }
    }

    public override void InitGame()
    {
        timer = 0.0f;
        isOver = false;
        successImage.SetActive(false);
        failedImage.SetActive(false);
        SetBlock();
    }
    
}
