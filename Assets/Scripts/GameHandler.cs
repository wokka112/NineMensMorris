using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private int blackPiecesLeft = 0;
    private int whitePiecesLeft = 0;

    [SerializeField]
    private LayerMask spaceLayer;
    [SerializeField]
    private LayerMask pieceLayer;
    [SerializeField]
    private GameObject blackPiece;
    [SerializeField]
    private GameObject whitePiece;

    private BoardState boardState;
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
        boardState = new BoardState(spaces, blackPiece, whitePiece, spaceLayer, pieceLayer);
        stateMachine = new GameStateMachine(boardState);
    }

    // Update is called once per frame
    void Update()
    {
        stateMachine.Process();
    }
}
