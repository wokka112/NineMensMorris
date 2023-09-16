using UnityEngine;

public class PlacePieceState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Place_Piece;

    private readonly GameController gameController;
    private readonly SetupStateMachine stateMachine;

    public PlacePieceState(SetupStateMachine stateMachine, GameController gameController)
    {
        this.gameController = gameController;
        this.stateMachine = stateMachine;
    }

    public ISetupState.SetupState GetSetupState()
    {
        return state;
    }

    public bool IsFinalState()
    {
        return false;
    }

    public void Process()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Space space = gameController.GetSpaceClicked(Input.mousePosition);
            if (space != null && space.IsSelectable())
            {
                Piece piece = gameController.PlacePiece(space);
                if (piece == null)
                {
                    // TODO replace with exception? But that'd break the game flow, whereas we can just ignore it and continue trying this way
                    Debug.LogError("Something went wrong while creating the new piece!");
                }
                else
                {
                    space.SetUnselectable();
                    if (piece.IsPartOfAMill())
                    {
                        gameController.MakeAllSpacesUnselectable();
                        stateMachine.SetCurrentState(ISetupState.SetupState.Remove_Piece);
                    } else
                    {
                        stateMachine.SetCurrentState(ISetupState.SetupState.Check_Setup_End);
                    }
                }
            }
        }
    }
}
