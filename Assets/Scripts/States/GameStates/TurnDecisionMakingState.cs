public class TurnDecisionMakingState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Decision_Making;

    private readonly GameStateMachine stateMachine;
    private readonly GameController gameController;

    public TurnDecisionMakingState(GameStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public IGameState.GameState GetGameState()
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
            stateMachine.SetCurrentState(IGameState.GameState.Game_End);
        } else if (gameController.GetSelectedPiece().IsPartOfAMill())
        {
            stateMachine.SetCurrentState(IGameState.GameState.Remove_Piece);
        } else
        {
            stateMachine.SetCurrentState(IGameState.GameState.Turn_End);
        }
        gameController.DeselectSelectedPiece();
    }
}
