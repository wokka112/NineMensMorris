using UnityEngine;

public class GameRunner : MonoBehaviour
{
    [SerializeField]
    private LayerMask spaceLayer;
    [SerializeField]
    private LayerMask pieceLayer;
    [SerializeField]
    private GameObject blackPiece;
    [SerializeField]
    private GameObject whitePiece;
    [SerializeField]
    private UiHandler uiHandler;

    private GameController gameController;
    private GameStateMachine stateMachine;
    private bool isPaused;

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] spaceObjects = GameObject.FindGameObjectsWithTag("Space");
        Space[] spaces = new Space[spaceObjects.Length]; 
        for (int i = 0; i < spaceObjects.Length; i++) {
            spaces[i] = spaceObjects[i].GetComponent<Space>();
        }
        gameController = new GameController(spaces, blackPiece, whitePiece, spaceLayer, pieceLayer, uiHandler);
        stateMachine = new GameStateMachine(gameController);

        stateMachine.AddListener(gameController);
        stateMachine.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (stateMachine.IsOnFinalState())
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (uiHandler.IsEndMenuDisplayed())
                {
                    uiHandler.HideEndMenu();
                }
                else
                {
                    uiHandler.DisplayEndMenu();
                }
            }
        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if (uiHandler.IsPauseMenuDisplayed())
                {
                    uiHandler.HidePauseMenu();
                    UnPauseGame();
                }
                else
                {
                    uiHandler.DisplayPauseMenu();
                    PauseGame();
                }
            }

            if (isPaused)
            {

            }
            else
            {
                stateMachine.Process();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void UnPauseGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}
