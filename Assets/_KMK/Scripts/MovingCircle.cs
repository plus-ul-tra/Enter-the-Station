using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

using DG.Tweening;

using UnityEngine.UI;

using UnityEngine.Splines.ExtrusionShapes;


public class MovingCircle : MonoBehaviour
{
    private MovingCircleManager movingCircleManager;

    public UnityEvent Success;
    public UnityEvent Fail;
    public UnityEvent ScopeStop;
    public Sprite rage;
    public Sprite smile;
    bool isStopped;
    private Image image;
    float circlePosX;
    Vector2 vectorCircle;
    float circleSpeed;

    public void OnEnable()
    {
        gameObject.transform.localPosition = new Vector3(290.0f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        image = GetComponent<Image>();
        movingCircleManager = transform.parent.GetComponent<MovingCircleManager>();

        vectorCircle.x = -1.0f;
        //circleSpeed = 0.0f;
        isStopped = false;
        image.sprite = rage;    
    }

    public void Balance() { circleSpeed = movingCircleManager.circleSpeed; }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //sStopped = true;
            ScopeStop.Invoke();
        }
        else if(!isStopped)
        {

            circlePosX = vectorCircle.x * circleSpeed * Time.fixedDeltaTime;
            gameObject.transform.Translate(circlePosX, 0, 0);
            //Debug.Log(circlePosX);

        }
    }

    void OnCollisionEnter2D(Collision2D collision) // BaseSqaure의 끝에 부딪혔을 때
    {
            if (vectorCircle.x > 0.0f) // 방향 벡터가 양수면 -1로 바꾼다
            {
                vectorCircle.x = -1.0f;
            }
            else if (vectorCircle.x < 0.0f) // 방향 벡터가 음수면 +1로 바꾼다
            {
                vectorCircle.x = 1.0f;
            }
        //Debug.Log(gameObject.transform.position.x);
    }
    public void SetSmile()
    {
        image.sprite = smile;
    }
    void OnTriggerEnter2D(Collider2D collision) // scopeSqaure와 겹칠 때
    {
        Success.Invoke();
        
    }
    void OnTriggerExit2D(Collider2D collision) // scopeSqaure와 안 겹칠 때
    {
        Fail.Invoke();
    }

    public void SetStop() { isStopped = true; }
}
