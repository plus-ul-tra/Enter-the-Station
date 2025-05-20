using UnityEngine;
using UnityEngine.UI;

public class pressSignGroup : MonoBehaviour
{
    void OnEnable()
    {
        for (int i = 0; i <= 1; i++) // 0���� 1�� �ڽ�
        {
            Transform group = transform.GetChild(i);
            group.gameObject.SetActive(true);

            for (int j = 0; j <= 4; j++) // �� �׷��� 5�� �ڽ�
            {
                Transform child = group.GetChild(j);
                child.gameObject.SetActive(false);
                child.localScale = Vector3.one;
            }

            // ��� ���� Image ���İ� �ʱ�ȭ
            Image[] images = group.GetComponentsInChildren<Image>();
            foreach (Image img in images)
            {
                img.canvasRenderer.SetAlpha(1f);
            }
        }
    }
}
