using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ClearCanvas : MonoBehaviour
{
    private StageManager _stageManager;
    
    public Button stageListBtn;
    public Button retryBtn;
    public Button nextBtn;

    private void Start()
    {
        _stageManager = FindAnyObjectByType<StageManager>();
        
        stageListBtn.onClick.AddListener(OnClickStageListBtn);
        retryBtn.onClick.AddListener(OnClickRetryBtn);
        nextBtn.onClick.AddListener(OnClickNextBtn);
    }

    public void OnClickStageListBtn()
    {
        SceneManager.LoadScene(0);
    }
    
    public void OnClickRetryBtn()
    {
        _stageManager.ResetStage();
    }
    
    public void OnClickNextBtn()
    {
        _stageManager.ChangeToNextStage();
    }
}
