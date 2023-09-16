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

    private GameController gameController;
    private GameStateMachine stateMachine;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game starting!");
        Debug.Log("Prepare to place your pieces!");
        GameObject[] spaceObjects = GameObject.FindGameObjectsWithTag("Space");
        Space[] spaces = new Space[spaceObjects.Length]; 
        for (int i = 0; i < spaceObjects.Length; i++) {
            spaces[i] = spaceObjects[i].GetComponent<Space>();
        }
        gameController = new GameController(spaces, blackPiece, whitePiece, spaceLayer, pieceLayer);
        stateMachine = new GameStateMachine(gameController);
    }

    // Update is called once per frame
    void Update()
    {
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
