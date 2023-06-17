using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private int blackPieces = 0;
    private int whitePieces = 0;
    private Space[] spaces;
    private STATE currentState = STATE.SETUP;
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
        if (currentState == STATE.SETUP)
        {
            // Get empty spaces
            foreach(Space space in spaces)
            {
                if (space.IsEmpty())
                {
                    // Highlight them
                    space.SetSelectable();
                }
            }
            
            // Player clicks on an empty space
            // We put a piece there
            // Increment players pieces count
            // Swap to other player
        }
    }

    private enum PLAYER
    {
        BLACK,
        WHITE
    }

    private enum STATE
    {
        SETUP,
        PLAY,
        WIN
    }
}
