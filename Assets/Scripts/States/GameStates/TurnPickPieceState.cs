using UnityEngine;

public class TurnPickPieceState : IState
{
    private const IState.State state = IState.State.Turn_Pick_Piece;

    private readonly GameStateMachine stateMachine;
    private readonly GameController gameController;

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
                    return;
                }

                gameController.SelectPiece(piece);
                gameController.MakeCurrentPlayersPiecesUnselectable();
                gameController.MakeSpacesPieceCanMoveToSelectable(piece);

                stateMachine.SetCurrentState(IState.State.Turn_Move_Piece);
            }
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
