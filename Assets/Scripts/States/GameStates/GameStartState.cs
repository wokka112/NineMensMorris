using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Game_Start;

    private readonly BoardState boardState;
    private readonly GameStateMachine gameStateMachine;

    public GameStartState(GameStateMachine gameStateMachine, BoardState boardState)
    {
        this.gameStateMachine = gameStateMachine;
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
            gameStateMachine.SetCurrentState(IGameState.GameState.Game_End);
        } else
        {
            gameStateMachine.SetCurrentState(IGameState.GameState.Turn_Start);
        }
    }
}
