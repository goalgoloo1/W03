using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Manager")]
    public static GameManager Instance;
    public StageManager StageManager => Instance._stageManager;
    private StageManager _stageManager;
    public CameraManager CameraManager => Instance._cameraManager;
    private CameraManager _cameraManager;
    
    [Header("Action")]
    public Action<int> OnSelectStageEvent;
    
    [Header("Properties")]
    public int SelectedStageNum { get; private set; }
    public List<StageData> StageDataList = new List<StageData>();
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(Instance);
    }

    private void Start()
    {
        _cameraManager = FindAnyObjectByType<CameraManager>();
        OnSelectStageEvent += LoadSceneWithTransition;
    }

    public void ChangeStageManager(StageManager newStageManager)
    {
        _stageManager = newStageManager;
    }
    
    public void LoadStageListScene()
    {
        SceneManager.LoadScene(1);
    }
    
    public void LoadStageScene(int index)
    {
        SelectedStageNum = index;
        SceneManager.LoadScene(index + 2);
    }

    public void LoadSceneWithTransition(int index)
    {
        StartCoroutine(CoLoadSceneWithIndex(index));
    }
    
    private IEnumerator CoLoadSceneWithIndex(int index)
    {
        yield return StartCoroutine(CameraManager.ActivateTransitionImage());
        LoadStageScene(index);
    }
}
