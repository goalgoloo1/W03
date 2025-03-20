using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    Button _newGameButton;
    Button _exitButton;

    private void Start()
    {
        _newGameButton = GameObject.Find("NewGameButton").GetComponent<Button>();
        _exitButton = GameObject.Find("ExitButton").GetComponent<Button>();

        _newGameButton.onClick.AddListener(NewGameButtonClick);
        _exitButton.onClick.AddListener(ExitButtonClick);
    }

    private void NewGameButtonClick()
    {
        SceneManager.LoadScene(1);
        Debug.Log("NewGame is started!!!");
    }
    private void ExitButtonClick()
    {
        Debug.Log("Exit Game!!");
        Application.Quit();
    }
    
}

