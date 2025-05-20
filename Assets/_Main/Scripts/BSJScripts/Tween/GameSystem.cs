using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameSystem
{

    /// <summary>
    /// TMP.Pro를 한글자씩 출력합니다.
    /// </summary>
    public static void DoText(this TMPro.TMP_Text text, string endValue, float duration, MonoBehaviour monoClass)
    {
        monoClass.StartCoroutine(CoText(text, endValue, duration));
    }

    static IEnumerator CoText(TMPro.TMP_Text text, string endValue, float duration)
    {

        WaitForSeconds charPerTime = new WaitForSeconds(duration / endValue.Length);
        string tempString = null;

        for (int i = 0; i < endValue.Length; i++)
        {
            tempString += endValue[i];
            text.text = tempString;

            yield return charPerTime;
        }
    }
}