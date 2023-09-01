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
        if (boardState.GetCurrentPlayer() == Player.WHITE)
        { 
            boardState.MakeWhitePiecesThatCanMoveSelectable();
        } else
        {
            boardState.MakeBlackPiecesThatCanMoveSelectable();
        }

        stateMachine.SetCurrentState(IGameState.GameState.Turn_Pick_Piece);
    }

    public IGameState.GameState GetState()
    {
        return state;
    }
}
