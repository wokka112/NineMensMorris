using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISetupState : IState
{
    ISetupState.SetupState GetSetupState();

    public enum SetupState
    {
        Highlight_Empty_Spaces,
        Place_Piece,
        Remove_Piece,
        Check_Setup_End,
        Final
    }
}
