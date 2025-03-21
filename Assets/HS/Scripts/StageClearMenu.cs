using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using System;
using Enums;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StageClearMenu : MonoBehaviour
{
    public Action OnStageClearMenuEvent;

    CanvasGroup _canvasGroup;
    RectTransform _clearPanelRect;
    TextMeshProUGUI _textRank;
    TextMeshProUGUI _textScore;
    TextMeshProUGUI _textClearTime;

    Button _returnToMainSceneBtn;
    Button _retryBtn;
    Button _playNextStageBtn;

    float _introPosX = 500; //px
    float _outroPosX = 1210; //PS: not used because no outro now.
    float _tweenDuration = 0.3f;
    //HS->DE: I added this line to get the selected stage number. 
    int _selectedStageNum = GameManager.Instance.SelectedStageNum; 




    private void Start()
    {

        Debug.Log("{GameManager.Instance.StageManager}");
        //StageClearSequence(); PS: for Debugging
        GameManager.Instance.StageManager.OnStageClearEvent += StageClearSequence;
    }

    
    private async void StageClearSequence(RankType type, float time, int coinCount)//RankType type, float time, int coinCount
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

        DisplayButtons();
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

        //button
        _returnToMainSceneBtn = GameObject.Find("ReturnToMainSceneBtn").GetComponent<Button>();
        _returnToMainSceneBtn.gameObject.SetActive(false);

        _playNextStageBtn = GameObject.Find("PlayNextStageBtn").GetComponent<Button>();
        _playNextStageBtn.gameObject.SetActive(false);

        _retryBtn = GameObject.Find("RetryBtn").GetComponent<Button>();
        _retryBtn.gameObject.SetActive(false);

        _returnToMainSceneBtn.onClick.AddListener(ReturnToMainSceneBtnClick);
        _playNextStageBtn.onClick.AddListener(PlayNextStageBtnClick);
        _retryBtn.onClick.AddListener(RetryBtnClick);
    }

    private void ReturnToMainSceneBtnClick()
    {
        //HS->DE: I didn't know what does MainScene mean, so Please change this code to the correct scene.  
        //HS->DE: MainScene이 6개 중 무엇을 의미하는지 모르겠습니다. 이 코드를 올바른 씬으로 변경부탁드립니다.
        //GameManager.Instance.OnSelectStageEvent?.Invoke(0);
        print("[HS->DE] Please change this code to the correct scene");
    }
    private void PlayNextStageBtnClick()
    {
        GameManager.Instance.OnSelectStageEvent?.Invoke(_selectedStageNum+1);
    }
    private void RetryBtnClick()
    {
        GameManager.Instance.OnSelectStageEvent?.Invoke(_selectedStageNum);
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

    private void DisplayButtons()
    {
        _returnToMainSceneBtn.gameObject.SetActive(true);
        _retryBtn.gameObject.SetActive(true);
        _playNextStageBtn.gameObject.SetActive(true);

    }

}
