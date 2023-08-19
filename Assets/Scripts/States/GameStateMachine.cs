using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine
{
    private IGameState previousState;
    private IGameState currentState;
    private BoardState boardState;
    private Dictionary<IGameState.GameState, IGameState> gameStates;

    public GameStateMachine(BoardState boardState)
    {
        this.boardState = boardState;
        SetupStates();
        SetCurrentState(IGameState.GameState.Init);
    }

    public void Process()
    {
        currentState.Process();
    }

    public void SetCurrentState(IGameState.GameState state)
    {
        Debug.Log("Setting current state to: " + state);
        gameStates.TryGetValue(state, out IGameState nextState);
        if (nextState == null)
        {
            Debug.LogError("No state exists for: " + state);
            // Switch to error state
        } else
        {
            SetCurrentState(nextState);
        }
    }

    public void SetCurrentState(IGameState nextState)
    {
        previousState = currentState;
        currentState = nextState;
    }

    public IGameState GetPreviousState()
    {
        return previousState;
    }

    private void SetupStates()
    {
        gameStates = new Dictionary<IGameState.GameState, IGameState>();

        IGameState initState = new InitState(this, boardState);
        gameStates.Add(initState.GetState(), initState);

        IGameState setupState = new BoardSetupState(this, boardState);
        gameStates.Add(setupState.GetState(), setupState);

        IGameState turnStartState = new TurnStartState(this, boardState);
        gameStates.Add(turnStartState.GetState(), turnStartState);

        IGameState turnSetupState = new TurnSetupState(this, boardState);
        gameStates.Add(turnSetupState.GetState(), turnSetupState);

        IGameState turnPickPieceState = new TurnPickPieceState(this, boardState);
        gameStates.Add(turnPickPieceState.GetState(), turnPickPieceState);

        IGameState turnMovePieceState = new TurnMovePieceState(this, boardState);
        gameStates.Add(turnMovePieceState.GetState(), turnMovePieceState);

        IGameState turnDecisionMakingState = new TurnDecisionMakingState(this, boardState);
        gameStates.Add(turnDecisionMakingState.GetState(), turnDecisionMakingState);

        IGameState turnEndState = new TurnEndState(this, boardState);
        gameStates.Add(turnEndState.GetState(), turnEndState);

        IGameState removePieceState = new RemovePieceState(this, boardState);
        gameStates.Add(removePieceState.GetState(), removePieceState);

        IGameState gameEndState = new GameEndState(this, boardState);
        gameStates.Add(gameEndState.GetState(), gameEndState);
    }
}
