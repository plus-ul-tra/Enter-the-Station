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

    void OnCollisionEnter2D(Collision2D collision) // BaseSqaure의 끝에 부딪혔을 때
    {
        if (vectorScope.x > 0.0f) // 방향 벡터가 양수면 -1로 바꾼다
        {
            vectorScope.x = -1.0f;
        }
        else if (vectorScope.x < 0.0f) // 방향 벡터가 음수면 +1로 바꾼다
        {
            vectorScope.x = 1.0f;
        }
    }

    public void SetStop()
    { isStopped = true; }
}
