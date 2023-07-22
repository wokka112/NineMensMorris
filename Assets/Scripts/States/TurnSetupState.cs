using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnSetupState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Setup;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnSetupState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        // Highlight selectable pieces.
        if (boardState.GetCurrentPlayer() == Player.WHITE)
        {
            // TODO change this so it only makes pieces that can move selectable
            boardState.MakeWhitePiecesSelectable();
        } else
        {
            boardState.MakeBlackPiecesSelectable();
        }

        stateMachine.SetCurrentState(IGameState.GameState.Turn_Pick_Piece);
    }

    public IGameState.GameState GetState()
    {
        return state;
    }
}
