using UnityEngine;

public class Spacebar : MonoBehaviour
{
    GameObject spacebarN;
    GameObject spacebarH;

    float time;
    bool isOver;

    void OnEnable()
    {
        spacebarN = transform.GetChild(0).gameObject;
        spacebarH = transform.GetChild(1).gameObject;

        spacebarN.SetActive(true);
        spacebarH.SetActive(false);

        time = 0f;
        isOver = false;
    }

    void Update()
    {
        if (isOver) return;

        time += Time.deltaTime;
        if (time >= 0.1f)
        {
            spacebarN.SetActive(false);
            spacebarH.SetActive(true);
        }
        if (time >= 0.2f)
        {
            spacebarN.SetActive(true);
            spacebarH.SetActive(false);

            time = 0f;
        }
    }

    public void SetisOver()
    {
        isOver = true;
    }
}
