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

    // Start is called before the first frame update
    void Start()
    {
        viewBoardButton.onClick.AddListener(ViewBoard);
        replayBtn.onClick.AddListener(Replay);
        mainMenuBtn.onClick.AddListener(MainMenu);
    }

    private void ViewBoard()
    {
        uiHandler.HideEndMenu();
    }

    private void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void MainMenu()
    {
        //TODO implement once we have a main menu scene
        Debug.Log("You clicked to go to the main menu!");
    }
}
