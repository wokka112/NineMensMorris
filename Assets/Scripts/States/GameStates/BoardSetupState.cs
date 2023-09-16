public class BoardSetupState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Board_Setup;

    private readonly GameStateMachine gameStateMachine;
    private readonly SetupStateMachine setupStateMachine;
    private readonly GameController gameController;

    public BoardSetupState(GameStateMachine gameStateMachine, GameController gameController)
    {
        this.gameStateMachine = gameStateMachine;
        this.gameController = gameController;
        setupStateMachine = new SetupStateMachine(gameController);
    }

    public void Process()
    {
        if (setupStateMachine.IsOnFinalState())
        {
            if (gameController.IsGameOver())
            {
                gameStateMachine.SetCurrentState(IGameState.GameState.Game_End);
            }
            else
            {
                gameStateMachine.SetCurrentState(IGameState.GameState.Game_Start);
            }
        }
        else
        {

            setupStateMachine.Process();
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
