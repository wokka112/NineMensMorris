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
    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return true;
    }

    public void Process()
    {
        AnnounceWinner();
    }

    private void AnnounceWinner()
    {
        Player? winner = boardState.GetWinner();
        if (winner == null)
        {
            throw new UnityException("Something went wrong! In game end state without a winner!!!");
        } else
        {
            Debug.Log(winner + " won!!!!");
        }
    }
}
