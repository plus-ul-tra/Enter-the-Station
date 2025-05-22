using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ResultPaper : MonoBehaviour
{
    [SerializeField]
    private TMP_Text tryCountText;
    [SerializeField]
    private TMP_Text clearCountText;
    [SerializeField]
    private TMP_Text claimCountText;
    [SerializeField]
    private TMP_Text itemCountText;
    [SerializeField]
    private TMP_Text totalScoreText;

    [Header("도장 이미지")]
    [SerializeField] private Image stampImage;

    [Header("도장 랭크 스프라이트")] // C B A S
    [SerializeField] private Sprite[] stampSprites;
    void Start()
    {
        ShowResult();
    }

    private void ShowResult()
    {
        if (tryCountText != null)
        {
            tryCountText.text = CountManager.Instance.GetTotalTry().ToString();
        }
        if (clearCountText != null)
        {
            clearCountText.text = CountManager.Instance.GetTotalClear().ToString();
        }
        if (claimCountText != null)
        {
            claimCountText.text = CountManager.Instance.GetClaim().ToString();

            switch(CountManager.Instance.GetClaim())
            {
                case 0:
                case 1:
                    if (CountManager.Instance.GetTotalItemCount() >= 10)
                    {
                        stampImage.sprite = stampSprites[0];
                    }
                    else
                    {
                        stampImage.sprite = stampSprites[1];
                    }
                    break;
                case 2:
                case 3:
                case 4:
                    stampImage.sprite = stampSprites[2];
                    break;
                case 5:
                case 6:
                case 7:
                    stampImage.sprite = stampSprites[3];
                    break;
                default:
                    stampImage.sprite = stampSprites[0];
                    break;
            }
        }
        if (itemCountText != null)
        {
            itemCountText.text = CountManager.Instance.GetTotalItemCount().ToString();
        }
        if (totalScoreText != null)
        {
            totalScoreText.text = CountManager.Instance.GetScore().ToString();
        }
    }
}
