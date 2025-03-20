using UnityEngine;
using UnityEngine.UI;

public class StageSelectMenu : MonoBehaviour
{
    Button _stageFirstButton;
    Button _stageSecondButton;
    Button _stageThirdButton;
    Button _stageFourthButton;

    private void Start()
    {
        StageSelectMenuSequence();
        Debug.Log("{GameManager.Instance.StageManager}");
        // OnStageClearMenuEvent += StageClearSequence;
        // OnStageClearMenuEvent?.Invoke();
    }

    private void StageSelectMenuSequence() 
    {
        _stageFirstButton = GameObject.Find("StageFirstButton").GetComponent<Button>();
        _stageSecondButton = GameObject.Find("StageSecondButton").GetComponent<Button>();
        _stageThirdButton = GameObject.Find("StageThirdButton").GetComponent<Button>();
        _stageFourthButton = GameObject.Find("StageFourthButton").GetComponent<Button>();

        _stageFirstButton.onClick.AddListener(StageFirstButtonClick);
        _stageSecondButton.onClick.AddListener(StageSecondButtonClick);
        _stageThirdButton.onClick.AddListener(StageThirdButtonClick);
        _stageFourthButton.onClick.AddListener(StageFourthButtonClick);
    }
    private void StageFirstButtonClick()
    {
        GameManager.Instance.OnSelectStageEvent?.Invoke(0);
    }
    private void StageSecondButtonClick()
    {
        GameManager.Instance.OnSelectStageEvent?.Invoke(1);
    }
    private void StageThirdButtonClick()
    {
        GameManager.Instance.OnSelectStageEvent?.Invoke(2);

    }
    private void StageFourthButtonClick()
    {
        GameManager.Instance.OnSelectStageEvent?.Invoke(3);

    }
}
