using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : StateMachine
{
    private readonly BoardState boardState;
    private Dictionary<IGameState.GameState, IState> gameStates;

    public GameStateMachine(BoardState boardState)
    {
        this.boardState = boardState;
        SetupStates();
        SetCurrentState(IGameState.GameState.Board_Setup);
    }

    public void SetCurrentState(IGameState.GameState state)
    {
        Debug.Log("Setting current state to: " + state);
        gameStates.TryGetValue(state, out IState nextState);
        if (nextState == null)
        {
            Debug.LogError("No state exists for: " + state);
            SetCurrentState(IGameState.GameState.Error);
        } else
        {
            SetCurrentState(nextState);
        }
    }

    private void SetupStates()
    {
        gameStates = new Dictionary<IGameState.GameState, IState>();

        IGameState setupState = new BoardSetupState(this, boardState);
        gameStates.Add(setupState.GetGameState(), setupState);

        IGameState turnStartState = new TurnStartState(this, boardState);
        gameStates.Add(turnStartState.GetGameState(), turnStartState);

        IGameState turnSetupState = new TurnSetupState(this, boardState);
        gameStates.Add(turnSetupState.GetGameState(), turnSetupState);

        IGameState turnPickPieceState = new TurnPickPieceState(this, boardState);
        gameStates.Add(turnPickPieceState.GetGameState(), turnPickPieceState);

        IGameState turnMovePieceState = new TurnMovePieceState(this, boardState);
        gameStates.Add(turnMovePieceState.GetGameState(), turnMovePieceState);

        IGameState turnDecisionMakingState = new TurnDecisionMakingState(this, boardState);
        gameStates.Add(turnDecisionMakingState.GetGameState(), turnDecisionMakingState);

        IGameState turnEndState = new TurnEndState(this, boardState);
        gameStates.Add(turnEndState.GetGameState(), turnEndState);

        IState removePieceState = new RemovePieceState(this, boardState, turnEndState);
        gameStates.Add(removePieceState.GetGameState(), removePieceState);

        IGameState gameEndState = new GameEndState(this, boardState);
        gameStates.Add(gameEndState.GetGameState(), gameEndState);
    }
}
