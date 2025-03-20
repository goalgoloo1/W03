using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using System;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
    [SerializeField] RectTransform pausePanelRect;
    [SerializeField] float topPosY, middlePosY;
    [SerializeField] float tweenDuration;
    [SerializeField] CanvasGroup canvasGroup; //Dark panel canvas group

    private async void Start()
    {
        
        await Task.Delay(2000);
        Debug.Log("미션 컴플릿또!!!");


        PausePanelIntro();
        await PausePanelOutro();
    }
    private void PausePanelIntro()
    {
        //canvasGroup.DOFade(1, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(middlePosY, tweenDuration).SetUpdate(true);
    }
    
    async Task PausePanelOutro()
    {
        await Task.Delay(2000);
        Debug.Log("PausePanelOutro");
        //canvasGroup.DOFade(0, tweenDuration).SetUpdate(true);
        pausePanelRect.DOAnchorPosY(topPosY, tweenDuration).SetUpdate(true);
    }
}
