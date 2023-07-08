using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField]
    private List<Space> neighbours;
    private List<Line> lines;

    private Transform spaceObject;
    private Renderer componentRenderer;
    private Piece piece;

    private bool isSelectable;

    private void Awake()
    {
        this.spaceObject = GetComponent<Transform>();
        this.componentRenderer = GetComponent<Renderer>();
        this.lines = new List<Line>();
    }

    public Vector3 GetPosition()
    {
        return spaceObject.position;
    }

    public bool IsEmpty()
    {
        return piece == null;
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
