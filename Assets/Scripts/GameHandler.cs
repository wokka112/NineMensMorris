using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private int blackPiecesPlaced = 0;
    private int whitePiecesPlaced = 0;
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
    private Space[] spaces;

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
                    space.SetPiece(CreatePiece(space.GetPosition(), currentPlayer));
                    // If player has 3 pieces in a row
                        // They remove opponent's piece
                    // Increment piece counters
                    if (currentPlayer == PLAYER.WHITE)
                    {
                        Debug.Log("Incrementing white pieces");
                        whitePiecesPlaced++;
                        whitePiecesLeft++;
                        Debug.Log("White pieces placed: " + whitePiecesPlaced + "\nWhite pieces left: " + whitePiecesLeft);
                        
                    } else
                    {
                        Debug.Log("Incrementing black pieces");
                        blackPiecesPlaced++;
                        blackPiecesLeft++;
                        Debug.Log("Black pieces placed: " + blackPiecesPlaced + "\nBlack pieces left: " + blackPiecesLeft);
                    }
                    // If both players have placed 9 pieces
                    if (currentPlayer == PLAYER.BLACK && blackPiecesPlaced == 9)
                    {
                        // Switch to play state
                        Debug.Log("Switching to PLAY state");
                        currentState = STATE.PLAY;
                    }
                    // Else
                     else
                    {
                        Debug.Log("Switching back to SETUP state");
                        currentState = STATE.SETUP_START;
                    }

                    MakeAllSpacesUnselectable();
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
            //TODO No component Piece atm. Either need to make Piece a MonoBehaviour or look into how to store data in game object without adding unnecessary scripts.
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
