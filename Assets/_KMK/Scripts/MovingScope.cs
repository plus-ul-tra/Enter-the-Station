using UnityEngine;

public class MovingScope : MonoBehaviour
{
    float ScopePosX;
    Vector2 vectorScope;
    public float ScopeSpeed;
    bool isStopped;

    public void OnEnable()
    {
        isStopped = false;
        vectorScope.x = -1.0f;
        transform.localPosition = new Vector3(-300.0f, transform.localPosition.y, transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    { 
        if (isStopped)
            return;

     ScopePosX = vectorScope.x * ScopeSpeed * Time.deltaTime;
     gameObject.transform.Translate(ScopePosX, 0, 0);
    }

    void OnCollisionEnter2D(Collision2D collision) // BaseSqaure�� ���� �ε����� ��
    {
        if (vectorScope.x > 0.0f) // ���� ���Ͱ� ����� -1�� �ٲ۴�
        {
            vectorScope.x = -1.0f;
        }
        else if (vectorScope.x < 0.0f) // ���� ���Ͱ� ������ +1�� �ٲ۴�
        {
            vectorScope.x = 1.0f;
        }
    }

    public void SetStop()
    { isStopped = true; }
}
