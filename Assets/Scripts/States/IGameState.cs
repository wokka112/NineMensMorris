using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGameState
{
    public void Process();

    public STATE GetState();

    public enum STATE
    {
        INIT,
        SETUP,
        PLAY,
        END
    }
}
