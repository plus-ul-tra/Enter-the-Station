using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines.ExtrusionShapes;


public class MovingCircle : MonoBehaviour
{
    public UnityEvent Success;
    public UnityEvent Fail;
    public UnityEvent ScopeStop;

    bool isStopped;
   
    float circlePosX;
    Vector2 vectorCircle;
    public float circleSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnEnable()
    {
        gameObject.transform.localPosition = new Vector3(480.0f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        vectorCircle.x = -1.0f;
        //circleSpeed = 0.0f;
        isStopped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isStopped = true;
            ScopeStop.Invoke();
        }
        else if(!isStopped)
        {
            circlePosX = vectorCircle.x * circleSpeed * Time.deltaTime;
            gameObject.transform.Translate(circlePosX, 0, 0);
            Debug.Log(circlePosX);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // BaseSqaure�� ���� �ε����� ��
    {
            if (vectorCircle.x > 0.0f) // ���� ���Ͱ� ����� -1�� �ٲ۴�
            {
                vectorCircle.x = -1.0f;
            }
            else if (vectorCircle.x < 0.0f) // ���� ���Ͱ� ������ +1�� �ٲ۴�
            {
                vectorCircle.x = 1.0f;
            }
        Debug.Log(gameObject.transform.position.x);
    }

    void OnTriggerEnter2D(Collider2D collision) // scopeSqaure�� ��ĥ ��
    {
        Success.Invoke();
    }
    void OnTriggerExit2D(Collider2D collision) // scopeSqaure�� �� ��ĥ ��
    {
        Fail.Invoke();
    }

    public void SetStop() { isStopped = true; }
}
