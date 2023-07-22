using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnEndState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_End;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnEndState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        if (boardState.GetSelectedPiece().IsPartOfAMill())
        {
            stateMachine.SetCurrentState(IGameState.GameState.Remove_Piece);
        } else
        {
            boardState.SwitchPlayer();
            stateMachine.SetCurrentState(IGameState.GameState.Turn_Start);
        }

        boardState.DeselectSelectedPiece();
    }

    public IGameState.GameState GetState()
    {
        return state;
    }
}
