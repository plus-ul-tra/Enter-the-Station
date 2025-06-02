
using UnityEngine;
using UnityEngine.Events;

public class MovingCircleLine : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    float time;
    public float speed;
    float circleSize;
    bool isStopped;
    bool isOver;

    void Update()
    {
        if (isOver) return;
        //if (isStopped) return; // ReachAim에서 Spacebar가 눌리면 CircleLine이 멈춤
        if (transform.localScale.x <= 0.0f) { Initalize(); }

        if (isStopped)
        { gameObject.transform.parent.transform.parent.gameObject.SetActive(false); } // SpaceBar가 눌리면 비활성화(사라짐)


        time += Time.deltaTime * speed;
        circleSize = Mathf.Lerp(1.0f, 0.0f, time);
        transform.localScale = new Vector3(circleSize, circleSize, circleSize);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "InnerCollider")
        {
            onTriggerExit.Invoke(); // ReachAim를 갖고 있는 객체의 isReached를 false로 바꿈
        }
        else if (collision.gameObject.name == "OuterCollider")
        {
            onTriggerEnter.Invoke(); // ReachAim를 갖고 있는 객체의 isReached를 true로 바꿈
        }
    }
    void OnDisable()
    {
        
    }
    public void OnEnable()
    {
        Initalize();
        MoveOtherSide();  
        isStopped = false;
        isOver = false;
    }
    void Initalize()
    {
        time = 0.0f;
        circleSize = 1.0f;
        transform.localScale = new Vector3(circleSize, circleSize, circleSize);
        onTriggerExit.Invoke(); // ReachAim를 갖고 있는 객체의 isReached를 false로 바꿈
    }
    void MoveOtherSide() // 사라졌을 때 반대편으로 이동하기
    {
        if(gameObject.transform.parent.transform.parent.transform.localPosition.x > 0.0f)
            gameObject.transform.parent.transform.parent.transform.localPosition = new Vector3(-218.0f, 0.0f, 0.0f);
        else if(gameObject.transform.parent.transform.parent.transform.localPosition.x < 0.0f)
            gameObject.transform.parent.transform.parent.transform.localPosition = new Vector3(218.0f, 0.0f, 0.0f);
    }
    public void SetIsStopped() // ReachAim을 갖고 있는 Spacebar 객체에서 사용
    {
        isStopped = true;
    }
    public void SetIsOver() // ReachAim을 갖고 있는 Spacebar 객체에서 사용
    {
        isOver = true;
    }
}
