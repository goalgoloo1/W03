using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using System;
using Enums;

public class StageClearMenu : MonoBehaviour
{
    // When player clear stage, start this event
    public Action OnStageClearMenuEvent;

    CanvasGroup _canvasGroup;
    RectTransform _clearPanelRect;
    TextMeshProUGUI _textRank;
    TextMeshProUGUI _textScore;
    TextMeshProUGUI _textClearTime;

    float _introPosX = 500; //px
    float _outroPosX = 1210; //px
    float _tweenDuration = 0.3f;

    private void Start()
    {

        Debug.Log("{GameManager.Instance.StageManager}");
        GameManager.Instance.StageManager.OnStageClearEvent += StageClearSequence;
        // OnStageClearMenuEvent += StageClearSequence;
        // OnStageClearMenuEvent?.Invoke();

    }
    
    private async void StageClearSequence(RankType type, float time, int coinCount)
    {
        FindUIObjects();
        ChangeClearUIText(type, time, coinCount);

        await Task.Delay(1000);

        ShowStageClearPanel();

        await Task.Delay(800);

        DisplayClearTime();

        await Task.Delay(500);

        DisplayScore();

        await Task.Delay(1000);

        DisplayRank();

        await Task.Delay(1800);

        HideStageClearPanel();

    }

    private void ChangeClearUIText(RankType type, float time, int coinCount)
    {
        _textRank.text = type.ToString();
        _textScore.text = coinCount.ToString();
        _textClearTime.text = time.ToString();
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
    }
    private void ShowStageClearPanel()
    {
        _canvasGroup.DOFade(0.5f, _tweenDuration).SetUpdate(true);
        _clearPanelRect.DOAnchorPosX(_introPosX, _tweenDuration).SetUpdate(true);
    }

    private void HideStageClearPanel()
    {
        _canvasGroup.DOFade(0, _tweenDuration).SetUpdate(true);
        _clearPanelRect.DOAnchorPosX(_outroPosX, _tweenDuration).SetUpdate(true);

        _textRank.gameObject.SetActive(false);
        _textClearTime.gameObject.SetActive(false);
        _textScore.gameObject.SetActive(false);
    }
    private void DisplayClearTime()
    {
        _textClearTime.gameObject.SetActive(true);
    }

    private void DisplayScore()
    {
        _textScore.gameObject.SetActive(true);
    }

    private void DisplayRank()
    {
        _textRank.gameObject.SetActive(true);
    }

}
