public class TurnDecisionMakingState : IState
{
    private const IState.State state = IState.State.Turn_Decision_Making;

    private readonly GameStateMachine stateMachine;
    private readonly GameController gameController;

    public TurnDecisionMakingState(GameStateMachine stateMachine, GameController gameController)
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
        if (gameController.IsGameOver())
        {
            stateMachine.SetCurrentState(IState.State.Game_End);
        } else if (gameController.GetSelectedPiece().IsPartOfAMill())
        {
            stateMachine.SetCurrentState(IState.State.Remove_Piece);
        } else
        {
            stateMachine.SetCurrentState(IState.State.Turn_End);
        }
        gameController.DeselectSelectedPiece();
    }
}
