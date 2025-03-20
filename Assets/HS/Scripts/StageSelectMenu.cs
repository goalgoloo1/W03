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
        //SceneManager.LoadScene("FirstScene"); 
        Debug.Log("LoadFirstScene");
    }
    private void StageSecondButtonClick()
    {
        //SceneManager.LoadScene("FirstScene"); 

        Debug.Log("Second");
    }
    private void StageThirdButtonClick()
    {
        //SceneManager.LoadScene("FirstScene"); 

        Debug.Log("Third");
    }
    private void StageFourthButtonClick()
    {
        //SceneManager.LoadScene("FirstScene"); 

        Debug.Log("Fourth");
    }
}
