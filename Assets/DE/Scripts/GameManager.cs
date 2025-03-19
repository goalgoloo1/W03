using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameData;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    // When selected stage UI
    public Action<int> OnSelectStageEvent;
    public int selectedStageNum { get; private set; }

    public StageDataContainter.StageList stageList = new StageDataContainter.StageList();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(instance);
    }

    private void Start()
    {
        OnSelectStageEvent += LoadSceneWithIndex;
    }

    public void LoadSceneWithIndex(int index)
    {
        Debug.Log($"LOAD SCENE : {index}");
        selectedStageNum = index;
        SceneManager.LoadScene(index + 1);
    }
}
