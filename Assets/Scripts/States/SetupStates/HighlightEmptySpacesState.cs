public class HighlightEmptySpacesState : ISetupState
{
    private static ISetupState.SetupState state = ISetupState.SetupState.Highlight_Empty_Spaces;

    private SetupStateMachine stateMachine;
    private GameController gameController;

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
