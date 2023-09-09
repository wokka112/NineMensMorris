using UnityEngine;

public class TurnPickPieceState : IGameState
{
    private const IGameState.GameState state = IGameState.GameState.Turn_Pick_Piece;

    private GameStateMachine stateMachine;
    private GameController gameController;

    public TurnPickPieceState(GameStateMachine stateMachine, GameController gameController)
    {
        this.stateMachine = stateMachine;
        this.gameController = gameController;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Piece piece = gameController.GetPieceClicked(Input.mousePosition);
            if (piece != null && piece.IsSelectable())
            {
                if(!piece.CanMove())
                {
                    Debug.Log("This piece can't move! Pick another!");
                    return;
                }

                gameController.SelectPiece(piece);
                gameController.MakeCurrentPlayersPiecesUnselectable();
                gameController.MakeSpacesPieceCanMoveToSelectable(piece);

                stateMachine.SetCurrentState(IGameState.GameState.Turn_Move_Piece);
            }
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
