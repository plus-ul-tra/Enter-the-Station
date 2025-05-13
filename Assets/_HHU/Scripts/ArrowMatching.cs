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
    private int successCount = 0; //9개 전부 맞추면 count++
    private List<ArrowButton> arrowBlocks = new List<ArrowButton>();
    private int matchIndex =0;

    private void OnEnable()
    {
        SetBlock();
    }

    // Update is called once per frame
    void Update()
    {
        if (arrowBlocks.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.UpArrow)) TryMatch(ArrowDir.Up);
        else if (Input.GetKeyDown(KeyCode.RightArrow)) TryMatch(ArrowDir.Right);
        else if (Input.GetKeyDown(KeyCode.DownArrow)) TryMatch(ArrowDir.Down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) TryMatch(ArrowDir.Left);
        checkSuccess();

        if(successCount == maxSuccessCount)
        {
            // task 성공
            //필요시 함수 추가
            Close();
            return;
        }
    }

    private void SetBlock()
    {
       // arrowBlocks.Clear();
        matchIndex = 0;
        for (int i=0; i< maxBlockCount-1; i++)
        {
            //별로 좋은 방법은 아닌거 같은데
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
        if(matchIndex == maxBlockCount-1)
        {
            successCount++;
        }
        if(matchIndex == maxBlockCount - 1 && successCount < maxSuccessCount)
        {
            ClearBlocks();
            SetBlock();
        }
    }
    private void TryMatch(ArrowDir inputDir)
    {   //방어코드
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
            Debug.Log("오답");
            //Block.MatcheFail();

        }
    }

    public override void InitGame()
    {
        SetBlock();
    }
    
}
