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
        boardState.SwitchPlayer();
        stateMachine.SetCurrentState(IGameState.GameState.Turn_Start);
    }

    public IGameState.GameState GetState()
    {
        return state;
    }
}
