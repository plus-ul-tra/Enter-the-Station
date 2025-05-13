using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ArrowButton : MonoBehaviour
{
    public ArrowDir Dir { get;private set; }
    private Image image;
    private float duration = 0.2f;   // 애니메이션 시간
    private float targetScale = 1.2f;  // 최종 스케일 배수
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
    { //맞았을때 처리 및 효과

        //안보이게만 변경
        //image.color = new Color(1, 1, 1, 0);
        MatchEffect();
    }
    public void MatcheFail()
    { //틀렸을때 처리 및 효과

    }
    public void MatchEffect()
    {
        // 이미지 초기 상태 설정
        image.transform.localScale = Vector3.one;

        // 동시에 스케일업 + 페이드아웃
        Sequence seq = DOTween.Sequence();

        seq.Join(image.transform.DOScale(targetScale, duration).SetEase(Ease.OutQuad));
        seq.Join(image.DOFade(0f, duration).SetEase(Ease.InQuad));

        // 끝난 뒤에 비활성화하고 싶다면
        //seq.OnComplete(() => Destroy(this.gameObject));

        seq.OnComplete(() => image.color = new Color(1, 1, 1, 0));
    }
    void Update()
    {
        
    }
}
