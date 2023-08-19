using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEndState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Game_End;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public GameEndState(GameStateMachine stateMachine, BoardState boardState)
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
        AnnounceWinner();
    }

    private void AnnounceWinner()
    {
        Player winner = boardState.GetWinner();
        Debug.Log(winner + " won!!!!");
    }
}
