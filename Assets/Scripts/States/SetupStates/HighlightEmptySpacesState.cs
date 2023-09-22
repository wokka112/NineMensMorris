public class HighlightEmptySpacesState : IState
{
    private const IState.State state = IState.State.Setup_Highlight_Empty_Spaces;

    private readonly StateMachine stateMachine;
    private readonly GameController gameController;

    public HighlightEmptySpacesState(StateMachine stateMachine, GameController gameController)
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
        gameController.MakeAllEmptySpacesSelectable();
        stateMachine.SetCurrentState(IState.State.Setup_Place_Piece);
    }
}
