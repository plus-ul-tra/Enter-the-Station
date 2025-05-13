using UnityEngine;

public class Task : MonoBehaviour
{
     public bool isComplete { get; protected set; }

    public virtual void InitGame() { } //외부에서 초기화 할 수 있으니 일단 public
    
    protected void Open() {
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true);
    }
    protected void Close() {
        gameObject.transform.parent.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }
}
