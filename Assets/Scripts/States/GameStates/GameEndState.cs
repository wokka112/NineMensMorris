public class GameEndState : IState
{
    private const IState.State state = IState.State.Game_End;

    private readonly GameController gameController;
    private readonly GameStateMachine stateMachine;

    public GameEndState(GameStateMachine stateMachine, GameController gameController)
    {
        this.gameController = gameController;
        this.stateMachine = stateMachine;
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
        stateMachine.SetCurrentState(IState.State.Final);
    }
}
