using UnityEngine;

public class TurnStartState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Start;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnStartState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        //TODO replace this with GUI stuff when we have one to say it's the player's turn.
        Debug.Log(boardState.GetCurrentPlayer() + "'s turn!");

        if (boardState.GetCurrentPlayer() == Player.WHITE)
        { 
            boardState.MakeWhitePiecesThatCanMoveSelectable();
        } else
        {
            boardState.MakeBlackPiecesThatCanMoveSelectable();
        }

        stateMachine.SetCurrentState(IGameState.GameState.Turn_Pick_Piece);
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
