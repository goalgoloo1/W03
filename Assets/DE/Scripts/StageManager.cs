using System;
using UnityEngine;
using Enums;

public class StageManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    public Action onInitStageEvent;
    public Action onPlayerDeathEvent;
    public Action onPlayerSuccessEvent;
    
    public Action<RankType, float, int> onChangeCleanUIEvent;
    
    [Header("Stage Data")]
    private int _stageIndex;
    private int _coinCount;
    
    public float TotalTime {get; private set;}
    public RankType CurrentFinalRank {get; private set;}
    public float CurrentFinalScore {get; private set;}
    
    private void Start()
    {
        onInitStageEvent += InitStage;
        
        onInitStageEvent?.Invoke();
    }

    private void InitStage()
    {
        _gameManager = GameManager.Instance;
        _gameManager.ChangeStageManager(this);

        _stageIndex = _gameManager.SelectedStageNum;
        CurrentFinalRank = _gameManager.StageDataList[_stageIndex].finlaRankType;
        TotalTime = _gameManager.StageDataList[_stageIndex].finalTotalTime;
        
        Debug.Log($"INIT {_stageIndex} STAGE");
    }
    
    public void ResetStage()
    {
        _gameManager.LoadSceneWithIndex(_stageIndex);
    }

    private void EndStage()
    {
        CurrentFinalScore = GetFinalScore(CurrentFinalScore, _coinCount);
        CurrentFinalRank = GetFinalRank(CurrentFinalScore);
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
    
    private void UpdateStageData()
    {
        if (CurrentFinalScore < _gameManager.StageDataList[_stageIndex].finalTotalScore)
        {
            return;
        }
        
        _gameManager.StageDataList[_stageIndex].finalTotalScore = CurrentFinalScore;
        _gameManager.StageDataList[_stageIndex].finlaRankType = CurrentFinalRank;
        _gameManager.StageDataList[_stageIndex].finalTotalTime = TotalTime;
    }
}
