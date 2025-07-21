using UnityEngine;

public class Spacebar_MovingCircle : MonoBehaviour
{
    GameObject spacebarN;
    GameObject spacebarH;

    float time;
    bool isIn;
    bool isOver;

    void OnEnable()
    {
        spacebarN = transform.GetChild(0).gameObject;
        spacebarH = transform.GetChild(1).gameObject;

        spacebarN.SetActive(true);
        spacebarH.SetActive(false);

        isIn = false;
        isOver = false;
    }

    void Update()
    {
        if (isOver) return;

        if (isIn)
        {
            spacebarN.SetActive(false);
            spacebarH.SetActive(true);
        }
        else
        {
            spacebarN.SetActive(true);
            spacebarH.SetActive(false);
        }
    }

    public void SetisOver()
    {
        isOver = true;
    }

    public void SetisIn() { isIn = true; }
    public void SetisOut() { isIn = false; }
}
