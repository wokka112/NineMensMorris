public class CheckSetupEndState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Check_Setup_End;

    private readonly GameController gameController;
    private readonly SetupStateMachine stateMachine;

    public CheckSetupEndState(SetupStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public ISetupState.SetupState GetSetupState()
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
            stateMachine.SetCurrentState(ISetupState.SetupState.Final);
        } else
        {
            stateMachine.SetCurrentState(ISetupState.SetupState.Highlight_Empty_Spaces);
        }

        gameController.SwitchPlayer();
    }
}
