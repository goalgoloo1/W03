using System;
using System.Collections;
using UnityEngine;
using Enums;

public class StageManager : MonoBehaviour
{
    private GameManager _gameManager;
    private InputManager _inputManager;
    
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
    private RankType _finalRank;
    private bool _isLastStage;
    
    private void Awake()
    {
        _gameManager = GameManager.Instance;
        
        
        // Action
        OnInitStageEvent += InitStage;
        
        OnGetCoinEvent += UpdateScore;
        OnPlayerDeathEvent += ResetStageWhenDied;
        

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

    private void Start()
    {
        _inputManager = InputManager.Instance;
        OnStageClearEvent += EndStage;
        _inputManager.ActivateMovement(true);
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
        _gameManager.ChangeStageManager(this);

        _stageIndex = _gameManager.SelectedStageNum;
        _finalRank = _gameManager.StageDataList[_stageIndex].FinalRankType;
        _currentFinalRank = RankType.UNRANKED;
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
        _inputManager.ActivateMovement(false);
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
        var finalRank = RankType.UNRANKED;
        switch (coin)
        {
            case 0:
                finalRank = RankType.B;
                break;
            case 1:
                finalRank = RankType.A;
                break;
            case 2:
                finalRank = RankType.S;
                break;
            case 3:
                finalRank = RankType.SS;
                break;
        }
        
        return finalRank;
    }

    private void UpdateStageData()
    {
        if (_currentFinalRank < _finalRank)
        {
            return;
        }
        
        _gameManager.StageDataList[_stageIndex].FinalRankType = _currentFinalRank;
        _gameManager.StageDataList[_stageIndex].FinalTotalTime = _totalTime;
        _gameManager.StageDataList[_stageIndex].FinalCoinCount = _coinCount;
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