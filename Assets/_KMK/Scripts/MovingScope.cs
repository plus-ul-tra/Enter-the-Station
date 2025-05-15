using UnityEngine;

public class MovingScope : MonoBehaviour
{
    Collider2D collider4Wall;
    float ScopePosX;
    Vector2 vectorScope;
    public float ScopeSpeed;

    void Start()
    {
        vectorScope.x = -1.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (MovingCircle.isStopped)
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
        Debug.Log(gameObject.transform.position.x);
    }
}
