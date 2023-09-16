public class TurnEndState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_End;

    private readonly GameStateMachine stateMachine;
    private readonly GameController gameController;

    public TurnEndState(GameStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public void Process()
    {
        if (gameController.IsGameOver())
        {
            stateMachine.SetCurrentState(IGameState.GameState.Game_End);
        } else { 
            gameController.SwitchPlayer();
            stateMachine.SetCurrentState(IGameState.GameState.Turn_Start);
        }
    }

    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }
}
