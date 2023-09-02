using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnDecisionMakingState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Decision_Making;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnDecisionMakingState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }

    public void Process()
    {
        if (boardState.IsGameOver())
        {
            stateMachine.SetCurrentState(IGameState.GameState.Game_End);
        } else if (boardState.GetSelectedPiece().IsPartOfAMill())
        {
            stateMachine.SetCurrentState(IGameState.GameState.Remove_Piece);
        } else
        {
            stateMachine.SetCurrentState(IGameState.GameState.Turn_End);
        }
        boardState.DeselectSelectedPiece();
    }
}
