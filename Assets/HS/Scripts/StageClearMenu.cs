using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using System;
using System.Threading;
using Enums;
using UnityEngine.UI;

public class StageClearMenu : MonoBehaviour
{
    public Action OnStageClearMenuEvent;

    CanvasGroup _canvasGroup;
    RectTransform _clearPanelRect;
    TextMeshProUGUI _textRank;
    TextMeshProUGUI _textScore;
    TextMeshProUGUI _textClearTime;

     Image _coinImg;
     Image _timeImg;

    Button _returnToMainSceneBtn;
    Button _retryBtn;
    Button _playNextStageBtn;

    float _introPosX = 500; //px
    float _outroPosX = 1210; //PS: not used because no outro now.
    float _tweenDuration = 0.3f;

    private CancellationTokenSource _cts;

    private void Start()
    {
        FindUIObjects();

        _cts = new CancellationTokenSource();

        //StageClearSequence(); PS: for Debugging
        GameManager.Instance.StageManager.OnStageClearEvent += StageClearSequence;
        
    }

    private void OnDestroy()
    {
        _cts?.Cancel();
        GameManager.Instance.StageManager.OnStageClearEvent -= StageClearSequence;
    }
    
    private async void StageClearSequence(RankType type, float time, int coinCount)//RankType type, float time, int coinCount
    {
        
        ChangeClearUIText(type, time, coinCount);

        await Task.Delay(1000);

        ShowStageClearPanel();

        await Task.Delay(800);

        DisplayClearTime();

        await Task.Delay(500);

        DisplayScore();

        await Task.Delay(1000);

        DisplayRank();

        DisplayButtons();
    }

    private void ChangeClearUIText(RankType type, float time, int coinCount)
    {
        _textRank.text = type.ToString();
        _textScore.text = $"{coinCount.ToString()} / 3";
        
        var minutes = Mathf.FloorToInt(time / 60f);
        var seconds = Mathf.FloorToInt(time % 60f);
        _textClearTime.text = $"{minutes:00}:{seconds:00}";
    }

    private void FindUIObjects()
    {
        _canvasGroup = transform.Find("DarkPanel").GetComponent<CanvasGroup>();
        _clearPanelRect = transform.Find("ClearPanel").GetComponent<RectTransform>();

        _textRank = GameObject.Find("Rank").GetComponent<TextMeshProUGUI>();
        _textRank.gameObject.SetActive(false);

        _textScore = GameObject.Find("Score").GetComponent<TextMeshProUGUI>();
        _textScore.gameObject.SetActive(false);

        _textClearTime = GameObject.Find("ClearTime").GetComponent<TextMeshProUGUI>();
        _textClearTime.gameObject.SetActive(false);

        //button
        _returnToMainSceneBtn = GameObject.Find("ReturnToMainSceneBtn").GetComponent<Button>();
        _returnToMainSceneBtn.gameObject.SetActive(false);

        _playNextStageBtn = GameObject.Find("PlayNextStageBtn").GetComponent<Button>();
        _playNextStageBtn.gameObject.SetActive(false);

        _retryBtn = GameObject.Find("RetryBtn").GetComponent<Button>();
        _retryBtn.gameObject.SetActive(false);
        
        _coinImg = GameObject.Find("CoinImg").GetComponent<Image>();
        _coinImg.gameObject.SetActive(false);
        
        _timeImg = GameObject.Find("TimeImg").GetComponent<Image>();
        _timeImg.gameObject.SetActive(false);

        _returnToMainSceneBtn.onClick.AddListener(ReturnToMainSceneBtnClick);
        _playNextStageBtn.onClick.AddListener(PlayNextStageBtnClick);
        _retryBtn.onClick.AddListener(RetryBtnClick);
        
    }

    private void ReturnToMainSceneBtnClick()
    {
        GameManager.Instance.StageManager.OnReturnToMainSceneEvent?.Invoke();
    }
    private void PlayNextStageBtnClick()
    {
        GameManager.Instance.StageManager.OnPlayNextStageEvent?.Invoke();
    }
    private void RetryBtnClick()
    {
        GameManager.Instance.StageManager.OnRetryStageEvent?.Invoke();
    }
    
    private void ShowStageClearPanel()
    {

        _canvasGroup.DOFade(0.5f, _tweenDuration).SetUpdate(true);
        _clearPanelRect.DOAnchorPosX(_introPosX, _tweenDuration).SetUpdate(true);
    }

    //commented out because it's not used
    //private void HideStageClearPanel()
    //{
    //    _canvasGroup.DOFade(0, _tweenDuration).SetUpdate(true);
    //    _clearPanelRect.DOAnchorPosX(_outroPosX, _tweenDuration).SetUpdate(true);

    //    _textRank.gameObject.SetActive(false);
    //    _textClearTime.gameObject.SetActive(false);
    //    _textScore.gameObject.SetActive(false);
    //}

    private void DisplayClearTime()
    {
        if (_textClearTime == null || _timeImg == null)
        {
            return;
        }
        
        _textClearTime.gameObject.SetActive(true);
        _timeImg.gameObject.SetActive(true);
    }

    private void DisplayScore()
    {
        if (_textScore == null || _coinImg == null)
        {
            return;
        }
        _textScore.gameObject.SetActive(true);
        _coinImg.gameObject.SetActive(true);
    }

    private void DisplayRank()
    {
        if (_textRank == null)
        {
            return;
        }
        
        _textRank.gameObject.SetActive(true);
    }

    private void DisplayButtons()
    {
        if (_textRank == null || _textScore == null || _textClearTime == null 
            || _coinImg == null || _timeImg == null)
        {
            return;
        }

        _returnToMainSceneBtn.gameObject.SetActive(true);
        _retryBtn.gameObject.SetActive(true);
        _playNextStageBtn.gameObject.SetActive(true);
    }
}
