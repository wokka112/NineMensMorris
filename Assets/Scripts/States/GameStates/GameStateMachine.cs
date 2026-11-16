using UnityEngine;

public class GameStateMachine : StateMachine
{
    private readonly GameController gameController;
    private readonly SetupStateMachine setupStateMachine;
    private readonly GameInputManager gameInputManager;

    public GameStateMachine(GameController gameController, GameInputManager gameInputManager)
    {
        this.gameController = gameController;
        this.gameInputManager = gameInputManager;
        setupStateMachine = new SetupStateMachine(gameController, gameInputManager);
    }

    public override void AddListener(IStateListener listener)
    {
        base.AddListener(listener);
        setupStateMachine.AddListener(listener);
    }

    public override void Init()
    {
        states.Clear();
        SetupStates();
        SetCurrentState(IState.State.Board_Setup);
        setupStateMachine.Init();
    }

    private void SetupStates()
    {
        IState setupState = new BoardSetupState(this, setupStateMachine, gameController);
        states.Add(setupState.GetState(), setupState);

        IState startState = new GameStartState(this, gameController);
        states.Add(startState.GetState(), startState);

        IState turnStartState = new TurnStartState(this, gameController);
        states.Add(turnStartState.GetState(), turnStartState);

        IState turnPickPieceState = new TurnPickPieceState(this, gameController, gameInputManager);
        states.Add(turnPickPieceState.GetState(), turnPickPieceState);

        IState turnMovePieceState = new TurnMovePieceState(this, gameController, gameInputManager);
        states.Add(turnMovePieceState.GetState(), turnMovePieceState);

        IState turnDecisionMakingState = new TurnDecisionMakingState(this, gameController);
        states.Add(turnDecisionMakingState.GetState(), turnDecisionMakingState);

        IState turnEndState = new TurnEndState(this, gameController);
        states.Add(turnEndState.GetState(), turnEndState);

        IState removePieceState = new RemovePieceState(this, gameController, gameInputManager, turnEndState.GetState());
        states.Add(removePieceState.GetState(), removePieceState);

        IState gameEndState = new GameEndState(this, gameController);
        states.Add(gameEndState.GetState(), gameEndState);

        IState finalState = new FinalState();
        states.Add(finalState.GetState(), finalState);
    }
}
