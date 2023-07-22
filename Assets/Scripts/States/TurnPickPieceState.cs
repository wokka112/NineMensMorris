using UnityEngine;

public class TurnPickPieceState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Pick_Piece;

    private GameStateMachine stateMachine;
    private BoardState boardState;

    public TurnPickPieceState(GameStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Piece piece = boardState.GetPieceClicked(Input.mousePosition);
            if (piece != null && piece.IsSelectable())
            {
                if(!piece.CanMove())
                {
                    Debug.Log("This piece can't move! Pick another!");
                    return;
                }

                boardState.SetSelectedPiece(piece);
                boardState.MakeCurrentPlayersPiecesUnselectable();
                boardState.MakeMovableSpacesSelectable(piece);
                stateMachine.SetCurrentState(IGameState.GameState.Turn_Move_Piece);
            }
        }
    }

    public IGameState.GameState GetState()
    {
        return state;
    }
}
