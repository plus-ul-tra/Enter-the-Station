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
    // Update is called once per frame
    void Update()
    {
        
    }
}
