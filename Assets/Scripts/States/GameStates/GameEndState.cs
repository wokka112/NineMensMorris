using UnityEngine;

public class GameEndState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Game_End;

    private readonly GameController gameController;
    private readonly GameStateMachine stateMachine;

    public GameEndState(GameStateMachine stateMachine, GameController gameController)
    {
        this.gameController = gameController;
        this.stateMachine = stateMachine;
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
        AnnounceWinner();
        stateMachine.SetCurrentState(IGameState.GameState.Final);
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
