using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public enum ArrowDir
{
    Up,
    Right,
    Down,
    Left,
}
public class ArrowMatching : Task
{
    public UnityEvent setZero;
    [SerializeField]
    private GameObject baseLineGroup;
    [SerializeField]
    private GameObject container;
    [SerializeField]
    private GameObject blockPrefabs;
    [SerializeField]
    private int maxBlockCount = 9;
    [SerializeField]
    public int maxSuccessCount = 3; // task success Complete
    public int successCount = 0; //9개 전부 맞추면 count++
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
            if (stageManager != null)
                stageManager.DecreasePlayerHp();
            timer = 0.0f;
            return;
        }

        if (successCount == maxSuccessCount && timer < limitTime)
        {
            // task 성공
            //필요시 함수 추가
            successImage.SetActive(true);
            CountManager.Instance.AddClearCount();
            timer = 0.0f;
            isOver = true;
            SoundManager.Instance.PlaySFX("Treat_DR_finish");
            Close();
            return;
        }

        if (arrowBlocks.Count == 0) return;

        if (Input.GetKeyDown(KeyCode.UpArrow)&&!isOver) TryMatch(ArrowDir.Up);
        else if (Input.GetKeyDown(KeyCode.RightArrow)&&!isOver) TryMatch(ArrowDir.Right);
        else if (Input.GetKeyDown(KeyCode.DownArrow)&&!isOver) TryMatch(ArrowDir.Down);
        else if (Input.GetKeyDown(KeyCode.LeftArrow)&&!isOver) TryMatch(ArrowDir.Left);
        checkSuccess();

        if (successCount >= 4) return;
        baseLineGroup.transform.GetChild(successCount).gameObject.SetActive(true); // 다음 baseline 켜기

    }

    private void SetBlock()
    {
       // arrowBlocks.Clear();
        matchIndex = 0;
        for (int i=0; i< maxBlockCount; i++)
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
        if(matchIndex == maxBlockCount && !isOver)
        {
            successCount++; 
            setZero.Invoke(); //drunken 이미지 원점으로 이동
        }
        if(matchIndex == maxBlockCount && successCount < maxSuccessCount)
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
            SoundManager.Instance.PlaySFX("Treat_DR_input1");
            matchIndex++;
        }
        else
        {
            SoundManager.Instance.PlaySFX("Fail_sound");
            //Debug.Log("오답");
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

        baseLineGroup.transform.GetChild(0).gameObject.SetActive(true); // 첫번째 자식은 켜두고
        for (int i = 1; i < baseLineGroup.transform.childCount; i++)
        {
            baseLineGroup.transform.GetChild(i).gameObject.SetActive(false); // 나머지 자식은 꺼둔다
        }
    }
    
}
