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
    // Move this into board state
    private Space[] spaces;
    private BoardState boardState;

    private STATE currentState = STATE.SETUP_START;
    private PLAYER currentPlayer = PLAYER.WHITE;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game starting!");
        Debug.Log("Prepare to place your pieces!");
        GameObject[] spaceObjects = GameObject.FindGameObjectsWithTag("Space");
        spaces = new Space[spaceObjects.Length]; 
        for (int i = 0; i < spaceObjects.Length; i++) {
            spaces[i] = spaceObjects[i].GetComponent<Space>();
        }
        boardState = new BoardState(spaces);
        Debug.Log("Spaces: " + spaces.Length);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == STATE.SETUP_START)
        {
            MakeAllEmptySpacesSelectable();
            currentState = STATE.SETUP_WAIT_FOR_CLICK;
        } else if (currentState == STATE.SETUP_WAIT_FOR_CLICK)
        {
            // Need event listener which waits for player click
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log("Left-click");
                Space space = GetSpaceClicked(Input.mousePosition);
                if (space.IsEmpty())
                {
                    Piece piece = CreatePiece(space.GetPosition(), currentPlayer);
                    piece.SetSpace(space);
                    space.SetPiece(piece);
                    space.SetUnselectable();
                    if (boardState.PieceMadeThreeInARow(piece))
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
                        MakeAllSpacesUnselectable();
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
                    // Make all spaces unselectable
                    // Swap players
                // Else
                    // Warn player that they need to pick an empty space
            // Else
                // Ignore
        }
    }

    //TODO move these make alls into board state
    private void MakeAllEmptySpacesSelectable()
    {
        foreach (Space space in spaces)
        {
            if (space.IsEmpty())
            {
                // Highlight them
                space.SetSelectable();
            }
        }
    }

    private void MakeAllSpacesUnselectable()
    {
        foreach (Space space in spaces)
        {
            space.SetUnselectable();
        }
    }

    private Space GetSpaceClicked(Vector3 mousePosition)
    {
        Space space = null;
        RaycastHit hitData;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hitData, 1000, spaceLayer))
        {
            Debug.Log("Hit object: " + hitData.transform.gameObject.name);
            space = hitData.transform.GetComponent<Space>();
        } else
        {
            Debug.Log("Didn't hit object!");
        }

        Debug.Log("Returning space: " + space.ToString());
        return space;
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
