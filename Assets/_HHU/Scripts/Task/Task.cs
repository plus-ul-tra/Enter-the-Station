using UnityEngine;

public class Task : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    public bool isOnTask { get; protected set; }  //���� ���� ������Ƽ ���µ� public?
    public KindOfTask kindOfTask;
    //[SerializeField]
    //protected float limitTime = 6.0f;
    //private float timer;

    public virtual void InitGame() { } //�ʱ�ȭ ����� Task���� �ٸ�. ������ �ƴ� ���״�� �ʱ�ȭ ���۽� �ҷ����� �ϴ� ��
    
    public void Open() { //���⼭ Ư�� ���ǿ� ���� ���� ������ �ش� collider�� �ȿ����� open�� �� �ִ�.
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true); //����
        isOnTask = true;
    }
    protected void Close() {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false); //����
        isOnTask = false;
    }
}
