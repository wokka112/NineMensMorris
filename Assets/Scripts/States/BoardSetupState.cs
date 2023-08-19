using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardSetupState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Board_Setup;

    private BoardState boardState;
    private GameStateMachine stateMachine;
    private int piecesPlaced = 0;
    private const int noOfPiecesToPlace = 9;

    public BoardSetupState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Space space = boardState.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsSelectable())
            {
                Piece piece = boardState.AddPieceToBoard(space);
                if (piece == null)
                {
                    Debug.LogError("Something went wrong while creating the new piece!");

                }
                else
                {
                    space.SetUnselectable();
                    if (piece.IsPartOfAMill())
                    {
                        boardState.MakeAllSpacesUnselectable();
                        stateMachine.SetCurrentState(IGameState.GameState.Remove_Piece);
                        return;
                    }

                    if (boardState.GetCurrentPlayer() == Player.BLACK)
                    {
                        piecesPlaced++;
                        if (piecesPlaced == noOfPiecesToPlace)
                        {
                            boardState.MakeAllSpacesUnselectable();
                            stateMachine.SetCurrentState(IGameState.GameState.Turn_Setup);
                        }
                    }

                    boardState.SwitchPlayer();
                }
            }
        }
    }

    public IGameState.GameState GetState()
    {
        return state;
    }   
}
