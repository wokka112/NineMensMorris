using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private int blackPiecesPlaced = 0;
    private int whitePiecesPlaced = 0;
    private int blackPiecesLeft = 0;
    private int whitePiecesLeft = 0;
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
            
        } else if (currentState == STATE.SETUP_WAIT_FOR_CLICK)
        {
            // Need event listener which waits for player click
            
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
