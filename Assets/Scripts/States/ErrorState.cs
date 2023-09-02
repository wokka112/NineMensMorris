using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorState : IState 
{
    public bool IsFinalState()
    {
        //TODO should this be true?
        return true;
    }

    public void Process()
    {
        Debug.LogError("An error occurred! If it keeps happening, please report it.");
    }
}
