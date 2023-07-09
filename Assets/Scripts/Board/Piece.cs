using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    [SerializeField]
    private Colour colour;
    
    private Space space;
    private Renderer componentRenderer;
    private bool isSelectable;

    public void Start()
    {
        this.componentRenderer = GetComponent<Renderer>();
    }

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

    public bool IsSelectable()
    {
        return isSelectable;
    }

    public void SetSelectable()
    {
        isSelectable = true;
        UpdateColour();
    }

    public void SetUnselectable()
    {
        isSelectable = false;
        UpdateColour();
    }

    private void UpdateColour()
    {
        componentRenderer.material.color = isSelectable ? Color.green : Color.black;
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
