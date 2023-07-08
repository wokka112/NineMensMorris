using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    private Colour colour;
    private Space space;

    public Colour GetColour()
    {
        return colour;
    }

    public Space GetSpace()
    {
        return space;
    }

    public void SetSpace(Space space)
    {
        this.space = space;
    }

    public bool IsPartOfAMill()
    {
        return space.IsPartOfAMill();
    }

    public enum Colour
    {
        WHITE,
        BLACK
    }
}
