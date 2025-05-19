using UnityEngine;
using UnityEngine.UI;
public class Task : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    public bool isOnTask { get; protected set; }  //���� ���� ������Ƽ ���µ� public?
    public KindOfTask kindOfTask;
    [SerializeField]
    protected float limitTime = 6.0f;
    protected float timer;
    private PlayerController playerController;
    private PlayerAnimator playerAnimator;
    private UIAction action;
    public GameObject successImage;
    public GameObject failedImage;

    public virtual void InitGame() { } //�ʱ�ȭ ����� Task���� �ٸ�. ������ �ƴ� ���״�� �ʱ�ȭ ���۽� �ҷ����� �ϴ� ��
    
    public void Open() { //���⼭ Ư�� ���ǿ� ���� ���� ������ �ش� collider�� �ȿ����� open�� �� �ִ�.
        
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true); //����
        

        isOnTask = true;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();

            var capsule = playerController.GetComponent<CapsuleCollider2D>();
            capsule.enabled = false;// �÷��̾���Ʈ�ѷ��� �����ϴ� �ݶ��̴� ������Ʈ off
        }
        else
        {
            Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�!");
        }
        playerController.canMove = false;
        //++�ִϸ��̼�
    }
    protected void Timer()
    {

    }
    protected void Close() {

        

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerAnimator = playerObject.GetComponent<PlayerAnimator>();
        }
        else
        {
            Debug.LogError("Player �±׸� ���� ������Ʈ�� ã�� �� �����ϴ�!");
            //return;
        }

        if (playerAnimator != null)
        {
            playerAnimator.SetStunned(false);
            playerAnimator.SetMoved(false);
            playerAnimator.SetMap(false);
            playerAnimator.SetClear(false);
            playerAnimator.SetFail(false);
            playerAnimator.SetWork(false);
            playerAnimator.SetFight(false);
            playerAnimator.SetSitting(false);
        }

        //close ���� ���� or ���� ȿ�� �� delay
        //Debug.Log("Call Close");
        action = gameObject.transform.parent.gameObject.GetComponent<UIAction>();
        action.HideAction(gameObject);
       // gameObject.SetActive(false); //����
        isOnTask = false;

        if(playerController)
        {
            playerController.canMove = true;

            var capsule = playerController.GetComponent<CapsuleCollider2D>();
            capsule.enabled = true;// �÷��̾���Ʈ�ѷ��� �����ϴ� �ݶ��̴� ������Ʈ on

            playerController = null;
        }
    }

    public float GetLimitTime()
    {
        return limitTime;
    }
}
