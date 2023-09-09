using UnityEngine;

public class GameEndState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Game_End;

    private GameController gameController;

    public GameEndState(GameController boardState)
    {
        gameController = boardState;
    }
    public IGameState.GameState GetGameState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return true;
    }

    public void Process()
    {
        AnnounceWinner();
    }

    private void AnnounceWinner()
    {
        Colour? winner = gameController.GetWinner();
        if (winner == null)
        {
            throw new UnityException("Something went wrong! In game end state without a winner!!!");
        } else
        {
            Debug.Log(winner + " won!!!!");
        }
    }
}
