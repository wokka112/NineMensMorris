using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    [SerializeField]
    private Button viewBoardButton;
    [SerializeField]
    private Button replayBtn;
    [SerializeField]
    private Button mainMenuBtn;
    [SerializeField]
    private UiHandler uiHandler;

    private AudioManager audioManager;

    private const string CLICK = "click";

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

        viewBoardButton.onClick.AddListener(ViewBoard);
        viewBoardButton.onClick.AddListener(PlayClick);

        replayBtn.onClick.AddListener(Replay);
        replayBtn.onClick.AddListener(PlayClick);

        mainMenuBtn.onClick.AddListener(MainMenu);
        mainMenuBtn.onClick.AddListener(PlayClick);
    }

    private void ViewBoard()
    {
        uiHandler.HideEndMenu();
    }

    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private void PlayClick()
    {
        audioManager.PlaySound(CLICK);
    }
}
