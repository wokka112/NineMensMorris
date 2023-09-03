public class BoardSetupState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Board_Setup;

    private readonly BoardState boardState;
    private GameStateMachine stateMachine;
    private SetupStateMachine setupStateMachine;

    public BoardSetupState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
        this.setupStateMachine = new SetupStateMachine(boardState);
    }

    public void Process()
    {
        if (setupStateMachine.IsOnFinalState())
        {
            stateMachine.SetCurrentState(IGameState.GameState.Game_Start);
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
