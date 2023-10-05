using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    //TODO implement volume controls once we have sound
    [SerializeField]
    private Slider volumeSlider;
    [SerializeField]
    private Button resumeBtn;
    [SerializeField]
    private Button quitBtn;
    [SerializeField]
    private GameRunner gameRunner;
    [SerializeField]
    private UiHandler uiHandler;

    // Start is called before the first frame update
    void Start()
    {
        resumeBtn.onClick.AddListener(Resume);
        quitBtn.onClick.AddListener(Quit);
    }

    private void Resume()
    {
        uiHandler.HidePauseMenu();
        gameRunner.UnPauseGame();
    }

    private void Quit()
    {
        //TODO make this revert to start menu scene once that's made.
        Debug.Log("You quit the game!");
    }
}
