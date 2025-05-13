using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public ArrowDir Dir { get;private set; }
    private Image image;

    private void OnEnable()
    {
        image = GetComponent<Image>();
    }
    public void SetButtonDir(ArrowDir buttondir)
    {
        Dir = buttondir;
        //ArrowDir dir = dir;
        switch (Dir)
        {
            case ArrowDir.Up:
                gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);
                image.color = Color.red;
                break;
            case ArrowDir.Right:
                gameObject.transform.Rotate(0.0f, 0.0f, 90.0f);
                image.color = Color.blue;
                break;
            case ArrowDir.Down:
                gameObject.transform.Rotate(0.0f, 0.0f, 0.0f);
                image.color = Color.green;
                break;
            case ArrowDir.Left:
                gameObject.transform.Rotate(0.0f, 0.0f, 270.0f);
                image.color = Color.yellow;
                break;

        }
    }
    public void Matched()
    { //�¾����� ó�� �� ȿ��

        //�Ⱥ��̰Ը� ����
        image.color = new Color(1, 1, 1, 0);
    }
    public void MatcheFail()
    { //Ʋ������ ó�� �� ȿ��

    }
    
    void Update()
    {
        
    }
}
