using System;
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
        OnSelectStageEvent += LoadSceneWithIndex;
    }

    public void ChangeStageManager(StageManager newStageManager)
    {
        _stageManager = newStageManager;
    }
    
    public void LoadStageListScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadSceneWithIndex(int index)
    {
        Debug.Log($"LOAD SCENE : {index}");
        SelectedStageNum = index;
        SceneManager.LoadScene(index + 2);
    }
}
