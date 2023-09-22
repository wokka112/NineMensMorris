using UnityEngine;

public class GameEndState : IState
{
    private const IState.State state = IState.State.Game_End;

    private readonly GameController gameController;
    private readonly GameStateMachine stateMachine;

    public GameEndState(GameStateMachine stateMachine, GameController gameController)
    {
        this.gameController = gameController;
        this.stateMachine = stateMachine;
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
        AnnounceWinner();
        stateMachine.SetCurrentState(IState.State.Final);
    }

    private void AnnounceWinner()
    {
        Colour? winner = gameController.GetWinner();
        if (winner == null)
        {
            Debug.LogError("Something went wrong! In game end state without a winner!!");
            throw new UnityException("Something went wrong! In game end state without a winner!!!");
        } else
        {
            Debug.Log(winner + " won!!!!");
        }
    }
}
