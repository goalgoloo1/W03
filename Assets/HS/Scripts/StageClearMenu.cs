using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;

public class StageClearMenu : MonoBehaviour
{
    CanvasGroup _canvasGroup;
    RectTransform _clearPanelRect;
    TextMeshProUGUI _textRank;
    TextMeshProUGUI _textScore;
    TextMeshProUGUI _textClearTime;


    float _introPosX = 500; //px
    float _outroPosX = 1210; //px
    float _tweenDuration = 0.3f;

    private async void Start()
    {
        UIObjectFind();

        await Task.Delay(1000);

        IntroStageClearPanel();

        await Task.Delay(1000);

        StageClearTime();

        await Task.Delay(1000);

        ScoreScroll();

        await Task.Delay(2000);

        ScaleRank();

        await Task.Delay(3000);

        OutroClearPanel();
    }
    private void UIObjectFind()
    {
        _canvasGroup = transform.Find("DarkPanel").GetComponent<CanvasGroup>();
        _clearPanelRect = transform.Find("ClearPanel").GetComponent<RectTransform>();

        _textRank = GameObject.Find("Rank").GetComponent<TextMeshProUGUI>();
        _textRank.gameObject.SetActive(false);

        _textScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        _textScore.gameObject.SetActive(false);

        _textClearTime = GameObject.Find("ClearTime").GetComponent<TextMeshProUGUI>();
        _textClearTime.gameObject.SetActive(false);


    }
    private void IntroStageClearPanel()
    {
        Debug.Log("IntroStageClearPanel");
        _canvasGroup.DOFade(0.5f, _tweenDuration).SetUpdate(true);
        _clearPanelRect.DOAnchorPosX(_introPosX, _tweenDuration).SetUpdate(true);
    }

    private void OutroClearPanel()
    {
        Debug.Log("OutroClearPanel");
        _canvasGroup.DOFade(0, _tweenDuration).SetUpdate(true);
        _clearPanelRect.DOAnchorPosX(_outroPosX, _tweenDuration).SetUpdate(true);
        _textRank.gameObject.SetActive(false); 
    }
    private void StageClearTime()
    {
        Debug.Log("클리어 시간: 00:01:44");
        _textClearTime.gameObject.SetActive(true);

    }

    private void ScoreScroll()
    {
        // score 스크롤링 효과 0.1초.
        Debug.Log("Score Scrolling......2만점 !!");
        _textScore.gameObject.SetActive(true);

        // 클리어시간 활성화
    }

    private void ScaleRank()
    {
        _textRank.gameObject.SetActive(true);
    }
    
}
