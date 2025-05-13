using UnityEngine;

public class Task : MonoBehaviour
{
     public bool isComplete { get; protected set; }

    public virtual void InitGame() { } //�ܺο��� �ʱ�ȭ �� �� ������ �ϴ� public
    
    protected void Open() {
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    protected void Close() {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
