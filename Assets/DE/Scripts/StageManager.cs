using System;
using System.Collections;
using UnityEngine;
using Enums;

public class StageManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    // When player 
    public Action OnInitStageEvent;
    public Action OnGetCoinEvent;
    public Action OnPlayerDeathEvent;
    public Action<RankType, float, int> OnStageClearEvent;

    // When player click clear UI buttons
    public Action OnReturnToMainSceneEvent;
    public Action OnRetryStageEvent;
    public Action OnPlayNextStageEvent;

    // Return stage data
    public Func<float> GetTotalTimeFloat;
    public Func<int> GetCoinCountInt;
    public Func<RankType> GetCurrentFinalRankRankType;
    
    [Header("Stage Data")]
    private bool _isStageClear = false;
    private int _stageIndex;
    private float _totalTime = 0;
    private int _coinCount = 0;
    private RankType _currentFinalRank;
    private bool _isLastStage;
    
    private void Awake()
    {
        // Action
        OnInitStageEvent += InitStage;
        OnGetCoinEvent += UpdateScore;
        OnPlayerDeathEvent += ResetStageWhenDied;
        OnStageClearEvent += EndStage;

        OnReturnToMainSceneEvent += ChangeToMainScene;
        OnRetryStageEvent += ResetStage;
        OnPlayNextStageEvent += ChangeToNextStage;
        
        // Func
        GetTotalTimeFloat += GetTotalTime;
        GetCoinCountInt += GetCoinCount;
        GetCurrentFinalRankRankType += GetCurrentFinalRank;
        
        // Initialize stage
        OnInitStageEvent?.Invoke();
    }
    
    private void Update()
    {
        // Start timer
        if (!_isStageClear)
        {
            _totalTime += Time.unscaledDeltaTime;
        }
    }

    private void InitStage()
    {
        _gameManager = GameManager.Instance;
        _gameManager.ChangeStageManager(this);

        _stageIndex = _gameManager.SelectedStageNum;
        _currentFinalRank = _gameManager.StageDataList[_stageIndex].FinalRankType;
        _isLastStage = _stageIndex == _gameManager.StageDataList.Count - 1;
        
        Debug.Log($"INIT {_stageIndex} STAGE");

        StartCoroutine(_gameManager.CameraManager.DeactivateTransitionImage());
    }
    
    private void ResetStageWhenDied()
    {
        StartCoroutine(CoResetStageWhenDied());
    }
    
    private IEnumerator CoResetStageWhenDied()
    {
        yield return StartCoroutine(_gameManager.CameraManager.CoCameraShake());
        ResetStage();
    }
    
    private void ResetStage()
    {
        _gameManager.LoadStageScene(_stageIndex);
    }

    private void EndStage(RankType type, float time, int coinCount)
    {
        _isStageClear = true;
        UpdateStageData();
    }
    
    private void ChangeToMainScene()
    {
        _gameManager.LoadStageListScene();
    }
    
    private void ChangeToNextStage()
    {
        if (_isLastStage)
        {
            Debug.Log("IT'S THE LAST STAGE!");
            return;
        }
        
        _gameManager.LoadSceneWithTransition(_stageIndex + 1);
    }
    
    private void UpdateScore()
    {
        _coinCount++; 
        _currentFinalRank = SetFinalRank(_coinCount);
    }

    private RankType SetFinalRank(int coin)
    {
        var finalRank = RankType.None;
        switch (coin)
        {
            case 0:
                finalRank = RankType.BRank;
                break;
            case 1:
                finalRank = RankType.ARank;
                break;
            case 2:
                finalRank = RankType.SRank;
                break;
            case 3:
                finalRank = RankType.SSRank;
                break;
        }
        
        return finalRank;
    }

    private void UpdateStageData()
    {
        if (_currentFinalRank < _gameManager.StageDataList[_stageIndex].FinalRankType)
        {
            return;
        }
        
        _gameManager.StageDataList[_stageIndex].FinalRankType = _currentFinalRank;
        _gameManager.StageDataList[_stageIndex].FinalTotalTime = _totalTime;
    }
    
    private float GetTotalTime()
    {
        return _totalTime;
    }
    
    private int GetCoinCount()
    {
        return _coinCount;
    }
    
    private RankType GetCurrentFinalRank()
    {
        return _currentFinalRank;
    }
}