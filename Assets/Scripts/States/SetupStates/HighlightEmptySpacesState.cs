public class HighlightEmptySpacesState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Highlight_Empty_Spaces;

    private readonly SetupStateMachine stateMachine;
    private readonly GameController gameController;

    public HighlightEmptySpacesState(SetupStateMachine stateMachine, GameController gameController)
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
        gameController.MakeAllEmptySpacesSelectable();
        stateMachine.SetCurrentState(ISetupState.SetupState.Place_Piece);
    }
}
