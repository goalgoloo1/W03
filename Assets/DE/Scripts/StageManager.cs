using System;
using UnityEngine;
using Enums;
using UnityEngine.UI;

public class StageManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    // When stage started
    public Action OnInitStageEvent;
    
    // When player get coin
    public Action OnGetCoinEvent;
    
    // When player died
    public Action OnPlayerDeathEvent;
    
    // When player succeed current stage
    public Action<RankType, float, int> OnStageClearEvent;

    // When player click clear UI buttons
    public Action OnReturnToMainSceneEvent;
    public Action OnRetryStageEvent;
    public Action OnPlayNextStageEvent;

    public Func<float> GetTotalTimeFloat;
    public Func<int> GetCoinCountInt;
    public Func<RankType> GetCurrentFinalRankRankType;
    
    [Header("Stage Data")]
    private bool _isStageClear = false;
    private int _stageIndex;
    private float _totalTime = 0;
    private int _coinCount = 0;
    private RankType _currentFinalRank;
    
    private void Awake()
    {
        // Action
        OnInitStageEvent += InitStage;
        OnGetCoinEvent += UpdateScore;
        OnPlayerDeathEvent += ResetStage;
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
        
        Debug.Log($"INIT {_stageIndex} STAGE");
    }
    
    private void ResetStage()
    {
        _gameManager.LoadSceneWithIndex(_stageIndex);
    }

    private void EndStage(RankType type, float time, int coinCount)
    {
        _isStageClear = true;
        UpdateStageData();
    }
    
    private void ChangeToMainScene()
    {
        _gameManager.LoadSceneWithIndex(0);
    }
    
    private void ChangeToNextStage()
    {
        _gameManager.LoadSceneWithIndex(_stageIndex + 1);
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