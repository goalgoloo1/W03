using System;
using UnityEngine;
using Enums;

public class StageManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    public Action OnInitStageEvent;
    public Action OnPlayerDeathEvent;
    public Action OnStageClearEvent;
    
    public Action<RankType, float, int> OnChangeClearUIEvent;
    public Func<RankType> GetCurrentFinalRankRankType;
    
    [Header("Stage Data")]
    private int _stageIndex;
    private int _coinCount;

    private float _totalTime;
    private RankType _currentFinalRank;
    private float _currentFinalScore;
    
    private void Start()
    {
        OnInitStageEvent += InitStage;
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

    private void EndStage()
    {
        _currentFinalScore = GetFinalScore(_currentFinalScore, _coinCount);
        _currentFinalRank = GetFinalRank(_currentFinalScore);
        UpdateStageData();
    }
    
    public void ChangeToNextStage()
    {
        _gameManager.LoadSceneWithIndex(_stageIndex + 1);
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

    private RankType GetCurrentFinalRank()
    {
        return _currentFinalRank;
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
}
