using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckSetupEndState : ISetupState
{
    private const ISetupState.SetupState state = ISetupState.SetupState.Check_Setup_End;
    private const int noOfPiecesToPlace = 9;

    private readonly BoardState boardState;
    private readonly SetupStateMachine stateMachine;

    public CheckSetupEndState(SetupStateMachine stateMachine, BoardState boardState)
    {
        this.stateMachine = stateMachine;
        this.boardState = boardState;
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
        if (boardState.GetBlackPiecesPlaced() >= noOfPiecesToPlace)
        {
            stateMachine.SetCurrentState(ISetupState.SetupState.Final);
        } else
        {
            stateMachine.SetCurrentState(ISetupState.SetupState.Highlight_Empty_Spaces);
        }

        boardState.SwitchPlayer();
    }
}
