public class GameStartState : IState
{
    private const IState.State state = IState.State.Game_Start;

    private readonly GameController gameController;
    private readonly GameStateMachine gameStateMachine;

    public GameStartState(GameStateMachine gameStateMachine, GameController gameController)
    {
        this.gameStateMachine = gameStateMachine;
        this.gameController = gameController;
    }

    public IState.State GetState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }

    public void Process()
    {
        gameStateMachine.SetCurrentState(IState.State.Turn_Start);
    }
}
