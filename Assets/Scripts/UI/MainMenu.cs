using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;
    [SerializeField]
    private GameObject optionsMenu;

    [SerializeField]
    private Button startButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button creditsButton;
    [SerializeField]
    private Button quitButton;

    private const string CLICK = "click";

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        startButton.onClick.AddListener(StartGame);
        startButton.onClick.AddListener(PlayClick);

        optionsButton.onClick.AddListener(ShowOptions);
        optionsButton.onClick.AddListener(PlayClick);

        creditsButton.onClick.AddListener(ViewCredits);
        creditsButton.onClick.AddListener(PlayClick);
        
        quitButton.onClick.AddListener(Quit);
        quitButton.onClick.AddListener(PlayClick);
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

    private void ViewCredits()
    {
        Debug.Log("View credits");
    }

    private void Quit()
    {
        Application.Quit();
    }

    private void PlayClick()
    {
        audioManager.PlaySound(CLICK);
    }
}
