using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;
using static UnityEditor.PlayerSettings;
using UnityEditor.U2D.Sprites;
using UnityEngine.UI;

public class DrunkenInArrow : MonoBehaviour
{
    public Sprite drunken1;
    public Sprite drunken2;
    public Sprite drunken3;
    Image thisImage;

    Rigidbody2D rb;

    Vector3 pos;
    Vector2 dirVector;
    Quaternion rotation;

    public float adjustSpeed;
    public float speed;
    float applySpeed;

    private ArrowMatching arrowMAtching;

    float time;
    float animTime;

    void OnEnable()
    {
        thisImage = GetComponent<Image>();
        thisImage.sprite = drunken1;

        rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localRotation = rotation;
        transform.localPosition = new Vector3(0f, 53.5f, 0f);
        dirVector = new Vector2(-1f, -1f);

        rb = GetComponent<Rigidbody2D>();
        rb.interpolation = RigidbodyInterpolation2D.Interpolate;
        rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
        rb.gravityScale = 0f;

        arrowMAtching = transform.parent.parent.GetComponent<ArrowMatching>();

        time = 0;
        animTime = 0f;
        applySpeed = speed;
    }

    void Update()
    {
        if (arrowMAtching.successCount == arrowMAtching.maxSuccessCount) return;

        ChangeSpeed();
        time += Time.deltaTime;
        if (time >= animTime) { ChangeAnim(); time = 0f; }
    }
    void FixedUpdate()
    {
        //Debug.Log("successCount: " + arrowMAtching.successCount + " maxSuccessCount: " + arrowMAtching.maxSuccessCount);
        if (arrowMAtching.successCount == arrowMAtching.maxSuccessCount) return;

        pos.x = transform.localPosition.x + (dirVector.x * applySpeed * 2.75f * Time.fixedDeltaTime);
        pos.y = -0.00116f * Mathf.Pow(pos.x, 2f) + 53.5f;

        transform.localPosition = pos;

        float angle = -pos.x * 0.002f * Mathf.Rad2Deg;
        // Clamp z값을 -45° ~ +45°로 제한
        angle = Mathf.Clamp(angle, -45, 45f);
        transform.localRotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 10f는 drunken 콜라이더의 반지름 길이
        if (dirVector.x > 0) { transform.localPosition = new Vector3(transform.localPosition.x - 10f, transform.localPosition.y - 10f, transform.localPosition.z); }
        else if (dirVector.x < 0) { transform.localPosition = new Vector3(transform.localPosition.x + 10f, transform.localPosition.y - 10f, transform.localPosition.z); }

        dirVector.x *= -1f; // 좌우만 반전
    }
    public void SetZero()
    {
        rotation = Quaternion.Euler(0f, 0f, 0f);
        transform.localPosition = new Vector3(0f, 53.5f, 0f);
    }

    void ChangeSpeed()
    {
        if (arrowMAtching.successCount == 0) { applySpeed = speed + (adjustSpeed * 0); }
        if (arrowMAtching.successCount == 1) { applySpeed = speed + (adjustSpeed * 1); }
        if (arrowMAtching.successCount == 2) { applySpeed = speed + (adjustSpeed * 2); }
        if (arrowMAtching.successCount == 3) { applySpeed = speed + (adjustSpeed * 3); }
    }
    void ChangeAnim()
    {
        if (arrowMAtching.successCount == 0) { animTime = 0.6f; ChangeImage(); }
        if (arrowMAtching.successCount == 1 && (arrowMAtching.successCount != arrowMAtching.maxSuccessCount)) { animTime = 0.8f; ChangeImage(); }
        if (arrowMAtching.successCount == 2 && (arrowMAtching.successCount != arrowMAtching.maxSuccessCount)) { animTime = 1f; ChangeImage(); }
        if (arrowMAtching.successCount == arrowMAtching.maxSuccessCount) { thisImage.sprite = drunken1; }
    }
    void ChangeImage()
    {
        if (thisImage.sprite == drunken1) { thisImage.sprite = drunken2; }
        else if (thisImage.sprite == drunken2) { thisImage.sprite = drunken3; }
        else if (thisImage.sprite == drunken3) { thisImage.sprite = drunken2; }
    }
}
