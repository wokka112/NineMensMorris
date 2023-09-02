using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacePieceState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Place_Piece;
    private const int noOfPiecesToPlace = 9;

    private readonly BoardState boardState;
    private SetupStateMachine stateMachine;
    private int piecesPlaced = 0;

    public PlacePieceState(SetupStateMachine stateMachine, BoardState boardState)
    {
        this.boardState = boardState;
        this.stateMachine = stateMachine;
    }

    public ISetupState.SetupState GetSetupState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Space space = boardState.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsSelectable())
            {
                Piece piece = boardState.PlacePiece(space);
                if (piece == null)
                {
                    // TODO replace with exception? But that'd break the game flow, whereas we can just ignore it and continue trying this way
                    Debug.LogError("Something went wrong while creating the new piece!");
                }
                else
                {
                    space.SetUnselectable();
                    if (piece.IsPartOfAMill())
                    {
                        boardState.MakeAllSpacesUnselectable();
                        stateMachine.SetCurrentState(ISetupState.SetupState.Remove_Piece);
                        return;
                    }

                    if (boardState.GetCurrentPlayer() == Player.BLACK)
                    {
                        piecesPlaced++;
                        if (piecesPlaced == noOfPiecesToPlace)
                        {
                            boardState.MakeAllSpacesUnselectable();
                            stateMachine.SetCurrentState(ISetupState.SetupState.Final);
                        }
                    }

                    boardState.SwitchPlayer();
                }
            }
        }
    }
}
