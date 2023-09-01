using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorState : IGameState 
{
    private const IGameState.GameState state = IGameState.GameState.Error;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public ErrorState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public IGameState.GameState GetState()
    {
        return state;
    }

    public void Process()
    {
        //TODO replace this with a GUI which tells the user an error occurred and to restart the game. Also need to log it somehow.
        Debug.LogError("An error occurred! If it keeps happening, please report it.");
    }
}
