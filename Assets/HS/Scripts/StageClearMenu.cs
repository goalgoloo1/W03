using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;

public class StageClearMenu : MonoBehaviour
{
    CanvasGroup canvasGroup;
    RectTransform clearPanelRect;


    float introPosX = 500; //px
    float outroPosX = 1210; //px
    float tweenDuration = 0.3f;

    private async void Start()
    {
        canvasGroup = transform.Find("DarkPanel").GetComponent<CanvasGroup>();

        clearPanelRect = transform.Find("ClearPanel").GetComponent<RectTransform>();

        await Task.Delay(1000);

        IntroClearPanel();

        await Task.Delay(1000);

        ScoreScroll();

        await Task.Delay(2000);

        ScaleRank();

        await Task.Delay(2000);

        OutroClearPanel();
    }
    private void IntroClearPanel()
    {
        Debug.Log("IntroClearPanel");
        canvasGroup.DOFade(0.5f, tweenDuration).SetUpdate(true);
        clearPanelRect.DOAnchorPosX(introPosX, tweenDuration).SetUpdate(true);
    }

    private void OutroClearPanel()
    {
        Debug.Log("OutroClearPanel");
        canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        clearPanelRect.DOAnchorPosX(outroPosX, tweenDuration).SetUpdate(true);
    }

    private void ScoreScroll()
    {
        // score 스크롤링 효과
        Debug.Log("Score Scrolling......");
    }

    private void ScaleRank() 
    {
        // rank가 스탬프처럼 나오도록 효과 
        Debug.Log("SSS Rank!!!");
    }

    
}
