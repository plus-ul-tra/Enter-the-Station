using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using UnityEngine.UI;

public class Drunken : MonoBehaviour
{
    public UnityEvent onTriggerEnter;
    public UnityEvent onTriggerExit;
    public UnityEvent onLeft;
    public UnityEvent onRight;

    public Sprite drunken1;
    public Sprite drunken2;
    public Sprite drunken3;
    Image thisImage;

    [SerializeField]
    private DrunkenManager drunkenManager;

    Rigidbody2D rb;

    Vector3 pos;
    Vector2 dirVector;
    Quaternion rotation;
    
    public float adjustSpeed;
    float speed;
    float applySpeed;

    float time;
    float animTime;
    bool isOver;
    

    void OnEnable()
    {
        thisImage = GetComponent<Image>();
        thisImage.sprite = drunken1;

        isOver = false;
        rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localRotation = rotation;
        transform.localPosition = new Vector3(0f, 53.5f, 0f);
        dirVector = new Vector2(-1f, -1f);

        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = 0f;

        time = 0;
        animTime = 0f;
        applySpeed = speed;
    }
    public void Balance() { speed = drunkenManager.swingSpeed; }  // 난이도 조정을 용이하게 하고자 추가된 부분
void Update()
    {
        ChangeSpeed();

        time += Time.deltaTime;
        if (time >= animTime) { ChangeAnim(); time = 0f; }
    }
    void FixedUpdate()
    {
        if (isOver) { thisImage.sprite = drunken1; return; }
       
        pos.x = transform.localPosition.x + (dirVector.x * applySpeed * 2.75f * Time.fixedDeltaTime);
        pos.y = -0.00116f * Mathf.Pow(pos.x, 2f) + 53.5f;

        transform.localPosition = pos;

        float angle = -pos.x * 0.002f * Mathf.Rad2Deg;
        // Clamp z값을 -45° ~ +45°로 제한
        angle = Mathf.Clamp(angle, -45f, 45f);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 10f는 drunken 콜라이더의 반지름 길이
        if (dirVector.x > 0) { transform.localPosition = new Vector3(transform.localPosition.x - 10f, transform.localPosition.y - 10f, transform.localPosition.z); }
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
    public void SetIsOver() { isOver = true; }// ReachAim을 갖고 있는 Spacebar 객체에서 사용

    void ChangeSpeed()
    {
        if (drunkenManager.countLevel == 0 || drunkenManager.countLevel == 1) { applySpeed = speed + (adjustSpeed * 0); }
        if (drunkenManager.countLevel == 2) { applySpeed = speed + (adjustSpeed * 1); }
        if (drunkenManager.countLevel == 3) { applySpeed = speed + (adjustSpeed * 2); }
        if (drunkenManager.countLevel == 4) { applySpeed = speed + (adjustSpeed * 3); }
    }
    void ChangeAnim()
    {
        if (drunkenManager.countLevel == 0 || drunkenManager.countLevel == 1) { animTime = 0.6f; ChangeImage(); }
        if (drunkenManager.countLevel == 2) { animTime = 0.8f; ChangeImage(); }
        if (drunkenManager.countLevel == 3) { animTime = 1f; ChangeImage(); }
        if (drunkenManager.countLevel == 4) { thisImage.sprite = drunken1; }
    }
    void ChangeImage()
    {
        if (thisImage.sprite == drunken1) { thisImage.sprite = drunken2; }
        else if (thisImage.sprite == drunken2) { thisImage.sprite = drunken3; }
        else if (thisImage.sprite == drunken3) { thisImage.sprite = drunken2; }
    }
}
