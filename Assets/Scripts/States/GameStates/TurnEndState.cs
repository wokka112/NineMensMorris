public class TurnEndState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_End;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnEndState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        if (boardState.IsGameOver())
        {
            stateMachine.SetCurrentState(IGameState.GameState.Game_End);
        } else { 
            boardState.SwitchPlayer();
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
