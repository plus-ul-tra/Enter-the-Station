using DG.Tweening;
using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public ArrowDir Dir { get;private set; }
    [SerializeField]
    private List<Sprite> buttonImages;
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
                //gameObject.transform.Rotate(0.0f, 0.0f, 180.0f);
                //image.color = Color.red;
                image.sprite = buttonImages[0];
                break;
            case ArrowDir.Right:
                
                image.sprite = buttonImages[1];
                break;
            case ArrowDir.Down:
                
                image.sprite = buttonImages[2];
                break;
            case ArrowDir.Left:
                
                image.sprite = buttonImages[3];
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
