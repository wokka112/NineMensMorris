using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField]
    private Button resumeBtn;
    [SerializeField]
    private Button optionsBtn;
    [SerializeField]
    private Button mainMenuBtn;
    [SerializeField]
    private GameRunner gameRunner;
    [SerializeField]
    private UiHandler uiHandler;

    // Start is called before the first frame update
    void Start()
    {
        resumeBtn.onClick.AddListener(Resume);
        optionsBtn.onClick.AddListener(ShowOptions);
        mainMenuBtn.onClick.AddListener(GoToMainMenu);
    }

    private void Resume()
    {
        uiHandler.HidePauseMenu();
        gameRunner.UnPauseGame();
    }

    private void ShowOptions()
    {
        uiHandler.HidePauseMenu();
        uiHandler.DisplayOptionsMenu();
    }

    private void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
