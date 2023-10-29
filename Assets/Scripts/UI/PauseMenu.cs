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

    private AudioManager audioManager;

    private const string CLICK = "click";

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        resumeBtn.onClick.AddListener(Resume);
        resumeBtn.onClick.AddListener(PlayClick);

        optionsBtn.onClick.AddListener(ShowOptions);
        optionsBtn.onClick.AddListener(PlayClick);

        mainMenuBtn.onClick.AddListener(GoToMainMenu);
        mainMenuBtn.onClick.AddListener(PlayClick);
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

    private void PlayClick()
    {
        audioManager.PlaySound(CLICK);
    }
}
