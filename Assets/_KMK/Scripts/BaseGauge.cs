using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class BaseGauge : Task
{
    public Image gauge;
    public GameObject successImage;
    public GameObject failedImage;

    public float addGauge; // 밖에서 조절
    public float subtractGauge;
    protected float time;
    protected float subTime;
    protected float closeTime;

    protected bool isClose;

    protected void SubGauge() // 0.1초마다 subtractGauge를 뺌
    {
        subTime += Time.deltaTime;
        if (subTime >= 0.1f)
        {
            subTime = 0.0f;
            gauge.fillAmount -= subtractGauge;
            //AnimSB(true); // 게이지가 깎일 동안은 스페이스바를 누르라는 애니메이션을 보여줌
        }
    }
    protected void SubGauge(float gaugeNum) // 0.1초마다 subtractGauge를 뺌
    {
            gauge.fillAmount -= gaugeNum;
    }

    // 스페이스바 애니메이션
}
