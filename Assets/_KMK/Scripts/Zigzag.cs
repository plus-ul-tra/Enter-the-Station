using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using static UnityEditor.PlayerSettings;
using UnityEditor.U2D.Sprites;

public class Zigzag : MonoBehaviour
{
   
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onLeft;
    public UnityEvent onRight;

    Vector3 pos;
    Quaternion rotation;
    Vector2 dirVector;
    public float speed;
    bool isOver;
    Rigidbody2D rb;
    [SerializeField]
    private ReachAim reachAim;
    private Vector2 dir = Vector2.left;

    public void OnEnable()
    {

        isOver = false;
        rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localRotation = rotation;
        transform.localPosition = new Vector3(0f, 53.5f, 0f);
        dirVector = new Vector2(-1f, -1f);

        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = 0f;
    }
    void Start()
    {
        OnEnable();
    }

    void Update()
    {
        if (reachAim.countLevel == 0 || reachAim.countLevel == 1) { speed = 0.5f; }
        if (reachAim.countLevel == 2) { speed = 0.45f; }
        if (reachAim.countLevel == 3) { speed = 0.4f; }
        if (reachAim.countLevel == 4) { speed = 0.35f; }
    }
    void FixedUpdate()
    {
        if (isOver) return;

        pos.x = transform.localPosition.x + (dirVector.x * speed * 2.75f);
        pos.y = -0.00116f * Mathf.Pow(pos.x, 2f) + 53.5f;

        transform.localPosition = pos;

        float angle = -pos.x * 0.002f * Mathf.Rad2Deg;
        // Clamp z값을 -45° ~ +45°로 제한
        angle = Mathf.Clamp(angle, -45f, 45f);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (dirVector.x > 0) { transform.localPosition = new Vector3(transform.localPosition.x - 10f, transform.localPosition.y - 10f, transform.localPosition.z); } // 10f는 콜라이더 지름 길이임
        else if (dirVector.x < 0) { transform.localPosition = new Vector3(transform.localPosition.x + 10f, transform.localPosition.y - 10f, transform.localPosition.z); }

        dirVector.x *= -1f; // 좌우만 반전
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        onTriggerEnter.Invoke();
        if (dirVector.x > 0) { onRight.Invoke(); } // ReachAim을 갖고있는 객체의 SetIsRight()
        else if (dirVector.x < 0) { onLeft.Invoke(); } // ReachAim을 갖고있는 객체의 SetIsLeft()
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        onTriggerExit.Invoke();
    }
    public void SetZero()
    {
        rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localPosition = new Vector3(0f, 55f, 0f);
    }
    public void SetIsOver() // ReachAim을 갖고 있는 Spacebar 객체에서 사용
    {
        isOver = true;
    }
}
