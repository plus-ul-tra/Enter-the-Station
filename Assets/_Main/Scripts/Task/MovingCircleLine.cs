
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
        //if (isStopped) return; // ReachAim���� Spacebar�� ������ CircleLine�� ����
        if (transform.localScale.x <= 0.0f) { Initalize(); }

        if (isStopped)
        { gameObject.transform.parent.transform.parent.gameObject.SetActive(false); } // SpaceBar�� ������ ��Ȱ��ȭ(�����)


        time += Time.deltaTime * speed;
        circleSize = Mathf.Lerp(1.0f, 0.0f, time);
        transform.localScale = new Vector3(circleSize, circleSize, circleSize);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "InnerCollider")
        {
            onTriggerExit.Invoke(); // ReachAim�� ���� �ִ� ��ü�� isReached�� false�� �ٲ�
        }
        else if (collision.gameObject.name == "OuterCollider")
        {
            onTriggerEnter.Invoke(); // ReachAim�� ���� �ִ� ��ü�� isReached�� true�� �ٲ�
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
        onTriggerExit.Invoke(); // ReachAim�� ���� �ִ� ��ü�� isReached�� false�� �ٲ�
    }
    void MoveOtherSide() // ������� �� �ݴ������� �̵��ϱ�
    {
        if(gameObject.transform.parent.transform.parent.transform.localPosition.x > 0.0f)
            gameObject.transform.parent.transform.parent.transform.localPosition = new Vector3(-218.0f, 0.0f, 0.0f);
        else if(gameObject.transform.parent.transform.parent.transform.localPosition.x < 0.0f)
            gameObject.transform.parent.transform.parent.transform.localPosition = new Vector3(218.0f, 0.0f, 0.0f);
    }
    public void SetIsStopped() // ReachAim�� ���� �ִ� Spacebar ��ü���� ���
    {
        isStopped = true;
    }
    public void SetIsOver() // ReachAim�� ���� �ִ� Spacebar ��ü���� ���
    {
        isOver = true;
    }
}
