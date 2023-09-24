public class GameStateMachine : StateMachine
{
    public GameStateMachine(GameController gameController) : base(gameController)
    {
        SetupStates();
        SetCurrentState(IState.State.Board_Setup);
    }

    private void SetupStates()
    {
        IState setupState = new BoardSetupState(this, gameController);
        states.Add(setupState.GetState(), setupState);

        IState startState = new GameStartState(this, gameController);
        states.Add(startState.GetState(), startState);

        IState turnStartState = new TurnStartState(this, gameController);
        states.Add(turnStartState.GetState(), turnStartState);

        IState turnPickPieceState = new TurnPickPieceState(this, gameController);
        states.Add(turnPickPieceState.GetState(), turnPickPieceState);

        IState turnMovePieceState = new TurnMovePieceState(this, gameController);
        states.Add(turnMovePieceState.GetState(), turnMovePieceState);

        IState turnDecisionMakingState = new TurnDecisionMakingState(this, gameController);
        states.Add(turnDecisionMakingState.GetState(), turnDecisionMakingState);

        IState turnEndState = new TurnEndState(this, gameController);
        states.Add(turnEndState.GetState(), turnEndState);

        IState removePieceState = new RemovePieceState(this, gameController, turnEndState.GetState());
        states.Add(removePieceState.GetState(), removePieceState);

        IState gameEndState = new GameEndState(this, gameController);
        states.Add(gameEndState.GetState(), gameEndState);

        IState finalState = new FinalState();
        states.Add(finalState.GetState(), finalState);
    }
}
