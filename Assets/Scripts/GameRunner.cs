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
        //TODO should we add the key press listening here for the options menu?
        // Then we could have a IsPaused variable in here and the UiHandler which would pause the processing of game states and ui.
        // Could also ignore it if we're on the final state.
        if (stateMachine.IsOnFinalState())
        {
            // TODO should we display a replay menu here? Or what? Or should that be dealt with in the state machine?
        }
        else
        {
            stateMachine.Process();
        }
    }
}
