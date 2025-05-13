using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseGauge : MonoBehaviour
{
    public Image gauge;
    public GameObject successImage;
    public GameObject failedImage;

    public float addGauge; // 밖에서 조절
    public float subtractGauge;
    protected float time;
    protected float subTime;

    void Start()
    {
        gauge.fillAmount = 0.0f;
        time = 0;
        successImage.SetActive(false);
        failedImage.SetActive(false);
    }
    protected void SubGauge() // 0.5초마다 subtractGauge를 뺌
    {
        subTime += Time.deltaTime;
        if (subTime >= 0.1f) // 게이지가 다 차지 않았으면
        {
            subTime = 0.0f;
            gauge.fillAmount -= subtractGauge;
            //AnimSB(true); // 게이지가 깎일 동안은 스페이스바를 누르라는 애니메이션을 보여줌
        }
    }
    //void AnimSB(bool a) // 스페이스바 애니메이션
    //{
    //    anim.SetBool("doWeHave2Press", a);
    //    //anim.SetTrigger("trigger");
    //}
}
