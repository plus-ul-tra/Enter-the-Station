using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SpaceBar : MonoBehaviour
{
    public Image gauge;
    public GameObject successImage;
    public GameObject failedImage;
    Animator anim;

    public float addGauge;
    public float subtractGauge;
    float time;
    float subTime;
    bool IsFilled;

    void Start()
    {
        gauge.fillAmount = 0.0f;
        time = 0;
        IsFilled = false;
        successImage.SetActive(false);
        failedImage.SetActive(false);
        
        anim = GetComponent<Animator>();
    }

    void Update()
    {    
        time += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && !IsFilled)
        {
            gauge.fillAmount += addGauge;
            if (gauge.fillAmount == 1.0f)
            {
                IsFilled = true;         
            }
            Debug.Log(IsFilled);
            Debug.Log(time);
        }
        else if (IsFilled && time <= 6.5f)
        {
            successImage.SetActive(true);
        }
        else if (!IsFilled && time >= 6.5f)
        {
            failedImage.SetActive(true);
        }

        if (time <= 6.5f) // 시간 오바되지 않는 한
        {
            SubGauge();
        }
    }

    void SubGauge() // 0.5초마다 subtractGauge를 뺌
    {
        subTime += Time.deltaTime;
        if (subTime >= 0.5f && !IsFilled) // 게이지가 다 차지 않았으면
        { 
            subTime = 0.0f;
            gauge.fillAmount -= subtractGauge;
            AnimSB(true); // 게이지가 깎일 동안은 스페이스바를 누르라는 애니메이션을 보여줌
        }
    }
    void AnimSB(bool a) // 스페이스바 애니메이션
    {
        anim.SetBool("doWeHave2Press" , a);
        //anim.SetTrigger("trigger");
    }
}
