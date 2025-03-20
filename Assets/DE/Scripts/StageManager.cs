using System;
using UnityEngine;
using Enums;

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

    public Func<float> GetTotalTimeFloat;
    public Func<int> GetCoinCountInt;
    public Func<RankType> GetCurrentFinalRankRankType;
    
    [Header("Stage Data")]
    private int _stageIndex;
    private float _totalTime;
    private int _coinCount;
    
    private RankType _currentFinalRank;
    private float _currentFinalScore;

    private void Start()
    {
        // Action
        OnInitStageEvent += InitStage;
        OnGetCoinEvent += UpdateScore;
        
        // Func
        GetTotalTimeFloat += GetTotalTime;
        GetCoinCountInt += GetCoinCount;
        GetCurrentFinalRankRankType += GetCurrentFinalRank;
        
        OnInitStageEvent?.Invoke();
    }

    private void InitStage()
    {
        _gameManager = GameManager.Instance;
        _gameManager.ChangeStageManager(this);

        _stageIndex = _gameManager.SelectedStageNum;
        _currentFinalRank = _gameManager.StageDataList[_stageIndex].finlaRankType;
        _totalTime = _gameManager.StageDataList[_stageIndex].finalTotalTime;
        
        Debug.Log($"INIT {_stageIndex} STAGE");
    }
    
    public void ResetStage()
    {
        _gameManager.LoadSceneWithIndex(_stageIndex);
    }
    
    public void ChangeToNextStage()
    {
        _gameManager.LoadSceneWithIndex(_stageIndex + 1);
    }
    
    private void UpdateScore()
    {
        _currentFinalScore += 10;
        _coinCount++; 
    }

    private float GetFinalScore(float time, int coinCount)
    {
        var score = 0;
        // Calculate Total Score
        return score;
    }

    private RankType GetFinalRank(float score)
    {
        var finalRank = RankType.None;
        // Calculate Final Rank
        return finalRank;
    }

    private void UpdateStageData()
    {
        if (_currentFinalScore < _gameManager.StageDataList[_stageIndex].finalTotalScore)
        {
            return;
        }
        
        _gameManager.StageDataList[_stageIndex].finalTotalScore = _currentFinalScore;
        _gameManager.StageDataList[_stageIndex].finlaRankType = _currentFinalRank;
        _gameManager.StageDataList[_stageIndex].finalTotalTime = _totalTime;
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
