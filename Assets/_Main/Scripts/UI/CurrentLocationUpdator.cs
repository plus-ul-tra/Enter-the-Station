using UnityEngine;
using TMPro;

public class CurrentLocationUpdator : MonoBehaviour
{
    [Header("현재 위치 TMP_텍스트")]
    [SerializeField] private TMP_Text curLocationText;

    private void Start()
    {
        if (curLocationText != null)
        {
            curLocationText.richText = true;
            ChangeLocationText(LocationType.b1);
        }
    }

    public void ChangeLocationText(LocationType locationType) 
    {
        switch(locationType)
        {
            case LocationType.b1:
                curLocationText.text = "현재 위치 : <color=#FF4800>지하 1층</color>";
                break;
            case LocationType.b2:
                curLocationText.text = "현재 위치 : <color=#eeea0f>지하 2층</color>";
                break;
        }
    }
}
