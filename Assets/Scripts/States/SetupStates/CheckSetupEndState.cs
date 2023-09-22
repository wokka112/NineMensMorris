public class CheckSetupEndState : IState
{
    private const IState.State state = IState.State.Setup_Check_Setup_End;

    private readonly GameController gameController;
    private readonly StateMachine stateMachine;

    public CheckSetupEndState(StateMachine stateMachine, GameController gameController)
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
        if (gameController.HaveAllPiecesBeenPlaced())
        {
            stateMachine.SetCurrentState(IState.State.Setup_Post_Setup);
        } else
        {
            stateMachine.SetCurrentState(IState.State.Setup_Highlight_Empty_Spaces);
        }

        gameController.SwitchPlayer();
    }
}
