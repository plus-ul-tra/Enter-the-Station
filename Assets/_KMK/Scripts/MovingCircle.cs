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
        isSuccess = true;
        Debug.Log(isSuccess);
    }
    void OnTriggerExit2D(Collider2D collision) // scopeSqaure�� �� ��ĥ ��
    {
        isSuccess = false;
        Debug.Log(isSuccess);
    }
}
