using UnityEngine;
using System.Collections;

public class Piece : MonoBehaviour
{
    [SerializeField]
    private Colour colour;
    
    private Space space;
    private Renderer componentRenderer;
    private Color originalColour;
    private bool isSelectable;

    public void Start()
    {
        this.componentRenderer = GetComponent<Renderer>();
        originalColour = componentRenderer.material.color;
    }

    public void Move(Space newSpace)
    {
        space.RemovePiece();
        SetSpace(newSpace);
        transform.position = newSpace.GetPosition() + new Vector3(0, 0.25f, 0);
        newSpace.SetPiece(this);
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
        componentRenderer.material.color = isSelectable ? Color.green : originalColour;
    }

    public bool IsPartOfAMill()
    {
        return space.IsPartOfAMill();
    }

    public bool CanMove()
    {
        IEnumerator enumerator = space.GetNeighbours().GetEnumerator();
        while (enumerator.MoveNext())
        {
            if (((Space) enumerator.Current).IsEmpty())
            {
                return true;
            }
        }

        return false;
    }

    public enum Colour
    {
        WHITE,
        BLACK
    }

    public override string ToString()
    {
        return colour + " piece. On space: " + space;
    }
}
