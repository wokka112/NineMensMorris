using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField]
    private List<Space> neighbours;
    // The lines this space is a part of
    private List<Line> lines;

    private Transform spaceObject;
    private Renderer componentRenderer;
    private Piece piece;

    private bool isSelectable;

    private void Awake()
    {
        spaceObject = GetComponent<Transform>();
        componentRenderer = GetComponent<Renderer>();
        lines = new List<Line>();
    }

    public List<Space> GetNeighbours()
    {
        return neighbours;
    }

    public Vector3 GetPosition()
    {
        return spaceObject.position;
    }

    public bool HasEmptyNeighbour()
    {
        foreach (Space space in neighbours)
        {
            if (space.IsEmpty())
            {
                return true;
            }
        }

        return false;
    }

    public bool IsEmpty()
    {
        return piece == null;
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

    public void SetPiece(Piece piece)
    {
        this.piece = piece;
    }

    public void RemovePiece()
    {
        piece = null;
    }

    public Piece GetPiece()
    {
        return piece;
    }

    public bool IsPartOfAMill()
    {
        foreach (Line line in lines)
        {
            if (line.IsAMill())
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateColour()
    {
        //TODO replace this with better rendering
        componentRenderer.material.color = isSelectable ? Color.green : Color.black;
    }

    public override string ToString()
    {
        return "Space(" + transform.position.x + "," + transform.position.z + ")";
    }

    public void AddLine(Line line)
    {
        lines.Add(line);
    }
}
