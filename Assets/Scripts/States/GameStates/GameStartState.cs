public class GameStartState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Game_Start;

    private readonly GameController gameController;
    private readonly GameStateMachine gameStateMachine;

    public GameStartState(GameStateMachine gameStateMachine, GameController gameController)
    {
        this.gameStateMachine = gameStateMachine;
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
        gameStateMachine.SetCurrentState(IGameState.GameState.Turn_Start);
    }
}
