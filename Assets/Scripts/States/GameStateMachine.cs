using System.Collections.Generic;
using UnityEngine;

public class GameStateMachine : StateMachine
{
    private readonly GameController gameController;
    private Dictionary<IGameState.GameState, IState> gameStates;

    public GameStateMachine(GameController gameController)
    {
        this.gameController = gameController;
        SetupStates();
        SetCurrentState(IGameState.GameState.Board_Setup);
    }

    public void SetCurrentState(IGameState.GameState state)
    {
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

        IGameState setupState = new BoardSetupState(this, gameController);
        gameStates.Add(setupState.GetGameState(), setupState);

        IGameState gameStartState = new GameStartState(this, gameController);
        gameStates.Add(gameStartState.GetGameState(), gameStartState);

        IGameState turnStartState = new TurnStartState(this, gameController);
        gameStates.Add(turnStartState.GetGameState(), turnStartState);

        IGameState turnPickPieceState = new TurnPickPieceState(this, gameController);
        gameStates.Add(turnPickPieceState.GetGameState(), turnPickPieceState);

        IGameState turnMovePieceState = new TurnMovePieceState(this, gameController);
        gameStates.Add(turnMovePieceState.GetGameState(), turnMovePieceState);

        IGameState turnDecisionMakingState = new TurnDecisionMakingState(this, gameController);
        gameStates.Add(turnDecisionMakingState.GetGameState(), turnDecisionMakingState);

        IGameState turnEndState = new TurnEndState(this, gameController);
        gameStates.Add(turnEndState.GetGameState(), turnEndState);

        IGameState removePieceState = new RemovePieceState(this, gameController, turnEndState);
        gameStates.Add(removePieceState.GetGameState(), removePieceState);

        IGameState gameEndState = new GameEndState(gameController);
        gameStates.Add(gameEndState.GetGameState(), gameEndState);
    }
}
