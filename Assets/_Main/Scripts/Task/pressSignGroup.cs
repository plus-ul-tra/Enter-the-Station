using UnityEngine;
using UnityEngine.UI;

public class pressSignGroup : MonoBehaviour
{
    void OnEnable()
    {
        for (int i = 0; i <= 1; i++) // 0번과 1번 자식
        {
            Transform group = transform.GetChild(i);
            group.gameObject.SetActive(true);

            for (int j = 0; j <= 4; j++) // 각 그룹의 5개 자식
            {
                Transform child = group.GetChild(j);
                child.gameObject.SetActive(false);
                child.localScale = Vector3.one;
            }

            // 모든 하위 Image 알파값 초기화
            Image[] images = group.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                img.canvasRenderer.SetAlpha(1f);
            }
        }
    }
}
