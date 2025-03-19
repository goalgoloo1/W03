using System;
using UnityEngine;
using Enums;

public class StageManager : MonoBehaviour
{
    private GameManager _gameManager;
    
    public Action onInitStageEvent;
    public Action onPlayerDeathEvent;
    public Action onPlayerSuccessEvent;
    
    [Header("Stage Data")]
    public int stageIndex;
    public int coinCount;
    public float totalTime;

    public RankType currentFinalRank;
    public float currentFinalScore;
    
    [Header("Spawn Object")]
    public ClearCanvas clearCanvasPrefab;
    private ClearCanvas _clearCanvas;
    
    [Header("Test Boolean")]
    public bool isPlayerDead;
    private bool _isPlayerDeadPrev;
    
    public bool isPlayerSuccess;
    private bool _isPlayerSuccessPrev;
    
    private void Start()
    {
        onInitStageEvent += InitStage;
        onPlayerDeathEvent += ResetStage;
        onPlayerSuccessEvent += EndStage;
        
        onInitStageEvent?.Invoke();
    }
    
    public void Update()
    {
        if (!isPlayerSuccess)
        {
            totalTime += Time.unscaledDeltaTime;
        }
        
        if (isPlayerDead != _isPlayerDeadPrev)
        {
            onPlayerDeathEvent?.Invoke();
            _isPlayerDeadPrev = isPlayerDead;
        }
        
        if (isPlayerSuccess != _isPlayerSuccessPrev)
        {
            onPlayerSuccessEvent?.Invoke();
            _isPlayerSuccessPrev = isPlayerSuccess;
        }
    }
    
    private void InitStage()
    {
        _gameManager = GameManager.instance;
        
        currentFinalRank = _gameManager.stageList.Stages[stageIndex].finlaRankType;
        totalTime = _gameManager.stageList.Stages[stageIndex].finalTotalTime;
    }
    
    public void ResetStage()
    {
        _gameManager.LoadSceneWithIndex(stageIndex);
    }

    private void EndStage()
    {
        currentFinalScore = GetFinalScore(currentFinalScore, coinCount);
        currentFinalRank = GetFinalRank(currentFinalScore);
        UpdateStageData();
        
        _clearCanvas = Instantiate(clearCanvasPrefab);
    }
    
    public void ChangeToNextStage()
    {
        _gameManager.LoadSceneWithIndex(stageIndex + 1);
    }

    private float GetFinalScore(float time, int coinCount)
    {
        var score = 0;
        // calculate TotalScore
        return score;
    }

    private RankType GetFinalRank(float score)
    {
        var finalRank = RankType.None;
        
        switch (score)
        {
            case 10:
                finalRank = RankType.BRank;
                break;
            case 20:
                finalRank = RankType.ARank;
                break;
            case 30:
                finalRank = RankType.SRank;
                break;
            case 40:
                finalRank = RankType.SSRank;
                break;
        }

        return finalRank;
    }
    
    private void UpdateStageData()
    {
        if (currentFinalScore < _gameManager.stageList.Stages[stageIndex].finalTotalScore)
        {
            return;
        }
        
        _gameManager.stageList.Stages[stageIndex].finalTotalScore = currentFinalScore;
        _gameManager.stageList.Stages[stageIndex].finlaRankType = currentFinalRank;
        _gameManager.stageList.Stages[stageIndex].finalTotalTime = totalTime;
    }
}
