public class BoardSetupState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Board_Setup;

    private GameStateMachine gameStateMachine;
    private SetupStateMachine setupStateMachine;

    public BoardSetupState(GameStateMachine gameStateMachine, GameController gameController)
    {
        this.gameStateMachine = gameStateMachine;
        setupStateMachine = new SetupStateMachine(gameController);
    }

    public void Process()
    {
        if (setupStateMachine.IsOnFinalState())
        {
            gameStateMachine.SetCurrentState(IGameState.GameState.Game_Start);
        }

        setupStateMachine.Process();
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
