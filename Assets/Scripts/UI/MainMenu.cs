using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button quitButton;

    void Start()
    {
        startButton.onClick.AddListener(StartGame);
        optionsButton.onClick.AddListener(ShowOptions);
        quitButton.onClick.AddListener(Quit);
    }

    private void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    private void ShowOptions()
    {
        gameObject.SetActive(false);
        optionsMenu.SetActive(true);
    }

    private void Quit()
    {
        Application.Quit();
    }
}
