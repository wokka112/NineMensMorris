public class PostState : IState
{
    private const IState.State state = IState.State.Setup_Post_Setup;

    private readonly GameController gameController;
    private readonly StateMachine stateMachine;

    public PostState(StateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
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
        gameController.MakeAllSpacesUnselectable();
        stateMachine.SetCurrentState(IState.State.Final);
    }
}
