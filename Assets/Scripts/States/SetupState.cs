using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupState : IGameState
{
    private const IGameState.STATE state = IGameState.STATE.SETUP;

    private BoardState boardState;
    private GameStateMachine stateMachine;
    private int piecesPlaced = 0;
    private const int noOfPiecesToPlace = 9;

    public SetupState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        //Debug.Log("Processing setup state");
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse clicked!");
            Space space = boardState.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsEmpty())
            {
                Piece piece = boardState.AddPieceToBoard(space);
                if (piece == null)
                {
                    Debug.LogError("Something went wrong while creating the new piec!");

                }
                else
                {
                    space.SetUnselectable();
                    if (piece.IsPartOfAMill())
                    {
                        Debug.Log("You made 3 in a row, woooo!");
                    }

                    if (boardState.GetCurrentPlayer() == Player.BLACK)
                    {
                        piecesPlaced++;
                        if (piecesPlaced == noOfPiecesToPlace)
                        {
                            Debug.Log("Switching to PLAY state");
                            stateMachine.SetCurrentState(IGameState.STATE.PLAY);
                        }
                    }

                    boardState.SwitchPlayer();
                }
            }
        }
    }

    public IGameState.STATE GetState()
    {
        return state;
    }   
}
