using UnityEngine;
using UnityEngine.UI;

public class StageListCanvas : MonoBehaviour
{
    private GameManager _gameManager;
    public Button stage0Btn;
    public Button stage1Btn;

    private void Start()
    {
        _gameManager = GameManager.instance;
        InitStageListCanvas();
    }

    private void InitStageListCanvas()
    {
        stage0Btn.onClick.AddListener(() => OnClickStageBtn(0));
        stage1Btn.onClick.AddListener(() => OnClickStageBtn(1));
    }
    
    private void OnClickStageBtn(int index)
    {
        GameManager.instance.OnSelectStageEvent?.Invoke(index);
    }
}
