using UnityEngine;
public class Task : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    public bool isOnTask { get; protected set; }  //���� ���� ������Ƽ ���µ� public?
    public KindOfTask kindOfTask;
    //[SerializeField]
    //protected float limitTime = 6.0f;
    //private float timer;
    private PlayerController playerController;
    private UIAction action;

    public virtual void InitGame() { } //�ʱ�ȭ ����� Task���� �ٸ�. ������ �ƴ� ���״�� �ʱ�ȭ ���۽� �ҷ����� �ϴ� ��
    
    public void Open() { //���⼭ Ư�� ���ǿ� ���� ���� ������ �ش� collider�� �ȿ����� open�� �� �ִ�.
        
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true); //����
        isOnTask = true;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();
        }
        else
        {
            Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�!");
        }
        playerController.canMove = false;
        //++�ִϸ��̼�
    }
    protected void Close() {
        //close ���� ���� or ���� ȿ�� �� delay
        Debug.Log("Call Close");
        action = gameObject.transform.parent.gameObject.GetComponent<UIAction>();
        action.HideAction(gameObject);
       // gameObject.SetActive(false); //����
        isOnTask = false;

        if(playerController)
        {
            playerController.canMove = true;
            playerController = null;
        }
    }
}
