using UnityEngine;

public class TurnStartState : IState
{
    private const IState.State state = IState.State.Turn_Start;

    private readonly GameStateMachine stateMachine;
    private readonly GameController gameController;

    public TurnStartState(GameStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public void Process()
    {
        gameController.MakeCurrentPlayersMovablePiecesSelectable();
        stateMachine.SetCurrentState(IState.State.Turn_Pick_Piece);
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
