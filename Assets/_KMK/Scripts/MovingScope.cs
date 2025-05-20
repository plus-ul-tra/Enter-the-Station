using DG.Tweening;
using UnityEngine;

public class MovingScope : MonoBehaviour
{
    float ScopePosX;
    Vector2 vectorScope;
    public float ScopeSpeed;
    bool isStopped;
    public float startPosX = -230f;

    public void OnEnable()
    {
        isStopped = false;
        vectorScope.x = -1.0f;

        transform.localPosition = new Vector3(startPosX, transform.localPosition.y, transform.localPosition.z);
    }
    public void RandomizeStartX(float minX, float maxX)
    {
        startPosX = Random.Range(minX, maxX);//x위치랜덤
        transform.localPosition = new Vector3(startPosX,
                                               transform.localPosition.y,
                                               transform.localPosition.z);

        vectorScope.x = (Random.value < 0.5f) ? -1f : 1f;//방향랜덤
    }
    // Update is called once per frame
    void Update()
    {
        if (isStopped)
            return;

        ScopePosX = vectorScope.x * ScopeSpeed * Time.deltaTime;
        gameObject.transform.localPosition = new Vector3(transform.localPosition.x + ScopePosX, transform.localPosition.y, transform.localPosition.z);
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