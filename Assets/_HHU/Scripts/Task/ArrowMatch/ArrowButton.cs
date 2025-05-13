using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public ArrowDir Dir { get;private set; }
    private Image image;
    private float duration = 0.2f;   // �ִϸ��̼� �ð�
    private float targetScale = 1.2f;  // ���� ������ ���
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
        //image.color = new Color(1, 1, 1, 0);
        MatchEffect();
    }
    public void MatcheFail()
    { //Ʋ������ ó�� �� ȿ��

    }
    public void MatchEffect()
    {
        // �̹��� �ʱ� ���� ����
        image.transform.localScale = Vector3.one;

        // ���ÿ� �����Ͼ� + ���̵�ƿ�
        Sequence seq = DOTween.Sequence();

        seq.Join(image.transform.DOScale(targetScale, duration).SetEase(Ease.OutQuad));
        seq.Join(image.DOFade(0f, duration).SetEase(Ease.InQuad));

        // ���� �ڿ� ��Ȱ��ȭ�ϰ� �ʹٸ�
        //seq.OnComplete(() => Destroy(this.gameObject));

        seq.OnComplete(() => image.color = new Color(1, 1, 1, 0));
    }
    void Update()
    {
        
    }
}
