using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    public void Process();

    public bool IsFinalState();
}
