using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class MovingCircle : MonoBehaviour
{
    bool isSuccess;
    public static bool isStopped;
    float circlePosX;
    Vector2 vectorCircle;
    public float circleSpeed;

    public GameObject SuccessImage;
    public GameObject FailedImage;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SuccessImage.SetActive(false);
        FailedImage.SetActive(false);
        vectorCircle.x = -1.0f;
        //circleSpeed = 0.0f;
        isStopped = false;
    }

    // Update is called once per frame
    void Update()
    { 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            circlePosX = 0;
            isStopped = true;
            if (isSuccess)
                SuccessImage.SetActive(true);
            else if (!isSuccess)
                FailedImage.SetActive(true);

        }
        else if(!isStopped)
        {
            circlePosX = vectorCircle.x * circleSpeed * Time.deltaTime;
            gameObject.transform.Translate(circlePosX, 0, 0);
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
        Debug.Log(gameObject.transform.position.x);
    }

    void OnTriggerEnter2D(Collider2D collision) // scopeSqaure와 겹칠 때
    {
        isSuccess = true;
        Debug.Log(isSuccess);
    }
    void OnTriggerExit2D(Collider2D collision) // scopeSqaure와 안 겹칠 때
    {
        isSuccess = false;
        Debug.Log(isSuccess);
    }
}
