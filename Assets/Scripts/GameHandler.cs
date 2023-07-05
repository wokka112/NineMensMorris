using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private int piecesPlaced = 0;
    private const int noOfPiecesToPlace = 9;
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

    private STATE currentState = STATE.SETUP_START;
    private PLAYER currentPlayer = PLAYER.WHITE;


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
        boardState = new BoardState(spaces, spaceLayer, pieceLayer);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == STATE.SETUP_START)
        {
            boardState.MakeAllEmptySpacesSelectable();
            currentState = STATE.SETUP_WAIT_FOR_CLICK;
        } else if (currentState == STATE.SETUP_WAIT_FOR_CLICK)
        {
            // Need event listener which waits for player click
            if (Input.GetMouseButtonDown(0))
            {
                //TODO how much of this to move into BoardState? Do I move instantiate into there via GameObject? Do I move all of this in there except for the state transitions?
                Space space = boardState.GetSpaceClicked(Input.mousePosition);
                if (space.IsEmpty())
                {
                    Piece piece = CreatePiece(space.GetPosition(), currentPlayer);
                    piece.SetSpace(space);
                    space.SetPiece(piece);
                    space.SetUnselectable();
                    if (boardState.PieceMadeAMill(piece))
                    {
                        Debug.Log("You made 3 in a row, woooo!");
                    }
                    if (currentPlayer == PLAYER.WHITE)
                    {
                        Debug.Log("Incrementing white pieces");
                        whitePiecesLeft++;
                        Debug.Log("White pieces left: " + whitePiecesLeft);
                        
                    } else
                    {
                        Debug.Log("Incrementing black pieces");
                        blackPiecesLeft++;
                        piecesPlaced++;
                        Debug.Log("Black pieces left: " + blackPiecesLeft);
                    }
                    // If both players have placed 9 pieces
                    if (piecesPlaced == noOfPiecesToPlace)
                    {
                        // Switch to play state
                        Debug.Log("Switching to PLAY state");
                        boardState.MakeAllSpacesUnselectable();
                        currentState = STATE.PLAY;
                    }
                    // Else
                     else
                    {
                        Debug.Log("Switching back to SETUP state");
                        currentState = STATE.SETUP_START;
                    }

                    if (currentPlayer == PLAYER.WHITE)
                    {
                        currentPlayer = PLAYER.BLACK;
                    } else
                    {
                        currentPlayer = PLAYER.WHITE;
                    }
                }
            }
            
            // When player clicks we get place they clicked
            // If space exists there
                // If space is selectable
                    // Place piece
                    // If player has 3 pieces in a row
                        // They remove opponent's piece
                    // Increment piece counters
                    // If both players have placed 9 pieces
                        // Switch to play state
                    // Else
                        // Switch to SETUP_START state
                    // Make all allSpaces unselectable
                    // Swap players
                // Else
                    // Warn player that they need to pick an empty space
            // Else
                // Ignore
        }
    }

    private Piece CreatePiece(Vector3 position, PLAYER player)
    {
        if (player == PLAYER.BLACK)
        {
            return Instantiate(blackPiece, position, Quaternion.identity).GetComponent<Piece>();
        } else
        {
            return Instantiate(whitePiece, position, Quaternion.identity).GetComponent<Piece>();
        }
    }

    private enum PLAYER
    {
        BLACK,
        WHITE
    }

    //TODO move states into classes and have them contain their own processing logic?
    // Could extend them off a State interface.
    private enum STATE
    {
        SETUP_START,
        SETUP_WAIT_FOR_CLICK,
        PLAY,
        END
    }
}
