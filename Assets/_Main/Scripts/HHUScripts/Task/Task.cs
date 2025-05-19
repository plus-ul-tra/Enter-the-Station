using UnityEngine;
using UnityEngine.UI;
public class Task : MonoBehaviour
{
    public bool isComplete { get; protected set; }
    public bool isOnTask { get; protected set; }  //현재 상태 프로퍼티 쓰는데 public?
    public KindOfTask kindOfTask;
    [SerializeField]
    protected float limitTime = 6.0f;
    protected float timer;
    private PlayerController playerController;
    private PlayerAnimator playerAnimator;
    private UIAction action;
    public GameObject successImage;
    public GameObject failedImage;

    public virtual void InitGame() { } //초기화 방식은 Task마다 다름. 시작이 아닌 말그대로 초기화 시작시 불러져야 하는 것
    
    public void Open() { //여기서 특정 조건에 대한 식을 넣으면 해당 collider의 안에서만 open될 수 있다.
        
        gameObject.transform.parent.gameObject.SetActive(true);
        gameObject.SetActive(true); //시작
        

        isOnTask = true;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerController = playerObject.GetComponent<PlayerController>();

            var capsule = playerController.GetComponent<CapsuleCollider2D>();
            capsule.enabled = false;// 플레이어컨트롤러에 존재하는 콜라이더 컴포넌트 off
        }
        else
        {
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
        }
        playerController.canMove = false;
        //++애니메이션
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
            Debug.LogError("Player 태그를 가진 오브젝트를 찾을 수 없습니다!");
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

        //close 전에 성공 or 실패 효과 및 delay
        //Debug.Log("Call Close");
        action = gameObject.transform.parent.gameObject.GetComponent<UIAction>();
        action.HideAction(gameObject);
       // gameObject.SetActive(false); //종료
        isOnTask = false;

        if(playerController)
        {
            playerController.canMove = true;

            var capsule = playerController.GetComponent<CapsuleCollider2D>();
            capsule.enabled = true;// 플레이어컨트롤러에 존재하는 콜라이더 컴포넌트 on

            playerController = null;
        }
    }

    public float GetLimitTime()
    {
        return limitTime;
    }
}
