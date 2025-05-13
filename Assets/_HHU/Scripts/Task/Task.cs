using UnityEngine;

public class Task : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    public bool isOnTask { get; protected set; }  //현재 상태 프로퍼티 쓰는데 public?
    public KindOfTask kindOfTask;
    //[SerializeField]
    //protected float limitTime = 6.0f;
    //private float timer;

    public virtual void InitGame() { } //초기화 방식은 Task마다 다름. 시작이 아닌 말그대로 초기화 시작시 불러져야 하는 것
    
    public void Open() { //여기서 특정 조건에 대한 식을 넣으면 해당 collider의 안에서만 open될 수 있다.
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true); //시작
        isOnTask = true;
    }
    protected void Close() {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false); //종료
        isOnTask = false;
    }
}
