using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private IGameState currentState;
    private BoardState boardState;
    private Dictionary<IGameState.STATE, IGameState> gameStates;


    public GameStateMachine(BoardState boardState)
    {
        this.boardState = boardState;
        SetupStates();
        SetCurrentState(IGameState.STATE.INIT);
    }

    public void Process()
    {
        currentState.Process();
    }

    public void SetCurrentState(IGameState.STATE state)
    {
        Debug.Log("Setting current state to: " + state);
        gameStates.TryGetValue(state, out IGameState nextState);
        if (nextState == null)
        {
            Debug.LogError("No state exists for: " + state);
            // Switch to error state
        } else
        {
            Debug.Log("State set");
            currentState = nextState;
        }
    }

    private void SetupStates()
    {
        gameStates = new Dictionary<IGameState.STATE, IGameState>();

        IGameState initState = new InitState(this, boardState);
        gameStates.Add(initState.GetState(), initState);

        IGameState setupState = new SetupState(this, boardState);
        gameStates.Add(setupState.GetState(), setupState);
    }
}
