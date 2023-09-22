public class TurnEndState : IState
{
    private const IState.State state = IState.State.Turn_End;

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
            stateMachine.SetCurrentState(IState.State.Game_End);
        } else { 
            gameController.SwitchPlayer();
            stateMachine.SetCurrentState(IState.State.Turn_Start);
        }
    }

    public IState.State GetState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }
}
