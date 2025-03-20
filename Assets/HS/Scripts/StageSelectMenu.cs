using UnityEngine;
using UnityEngine.UI;

public class StageSelectMenu : MonoBehaviour
{
    Button StageFirstButton;
    Button StageSecondButton;
    Button StageThirdButton;
    Button StageFourthButton;

    private void Start()
    {
        StageFirstButton = GameObject.Find("StageFirstButton").GetComponent<Button>();
        StageSecondButton = GameObject.Find("StageSecondButton").GetComponent<Button>();
        StageThirdButton = GameObject.Find("StageThirdButton").GetComponent<Button>();
        StageFourthButton = GameObject.Find("StageFourthButton").GetComponent<Button>();

        StageFirstButton.onClick.AddListener(StageFirstButtonClick);
        StageSecondButton.onClick.AddListener(StageSecondButtonClick);
        StageThirdButton.onClick.AddListener(StageThirdButtonClick);
        StageFourthButton.onClick.AddListener(StageFourthButtonClick);
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
