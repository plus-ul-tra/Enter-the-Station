using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.Splines.ExtrusionShapes;


public class MovingCircle : MonoBehaviour
{
    public UnityEvent Success;
    public UnityEvent Fail;
    public UnityEvent ScopeStop;
    public Sprite rage;
    public Sprite smile;
    bool isStopped;
    private Image image;
    float circlePosX;
    Vector2 vectorCircle;
    public float circleSpeed;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnEnable()
    {
        gameObject.transform.localPosition = new Vector3(290.0f, gameObject.transform.localPosition.y, gameObject.transform.localPosition.z);
        image = GetComponent<Image>();
        vectorCircle.x = -1.0f;
        //circleSpeed = 0.0f;
        isStopped = false;
        image.sprite = rage;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //sStopped = true;
            ScopeStop.Invoke();
        }
        else if(!isStopped)
        {
            circlePosX = vectorCircle.x * circleSpeed * Time.deltaTime;
            gameObject.transform.Translate(circlePosX, 0, 0);
            //Debug.Log(circlePosX);
        }
    }

    void OnCollisionEnter2D(Collision2D collision) // BaseSqaureÀÇ ³¡¿¡ ºÎµúÇûÀ» ¶§
    {
            if (vectorCircle.x > 0.0f) // ¹æÇâ º¤ÅÍ°¡ ¾ç¼ö¸é -1·Î ¹Ù²Û´Ù
            {
                vectorCircle.x = -1.0f;
            }
            else if (vectorCircle.x < 0.0f) // ¹æÇâ º¤ÅÍ°¡ À½¼ö¸é +1·Î ¹Ù²Û´Ù
            {
                vectorCircle.x = 1.0f;
            }
        //Debug.Log(gameObject.transform.position.x);
    }
    public void SetSmile()
    {
        image.sprite = smile;
    }
    void OnTriggerEnter2D(Collider2D collision) // scopeSqaure¿Í °ãÄ¥ ¶§
    {
        Success.Invoke();
        
    }
    void OnTriggerExit2D(Collider2D collision) // scopeSqaure¿Í ¾È °ãÄ¥ ¶§
    {
        Fail.Invoke();
    }

    public void SetStop() { isStopped = true; }
}
