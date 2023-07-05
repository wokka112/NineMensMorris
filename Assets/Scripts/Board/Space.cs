using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField]
    private List<Space> neighbours;
    [SerializeField]
    private LinePosition[] lines = new LinePosition[2];

    private Transform spaceObject;
    private Renderer componentRenderer;
    private Piece piece;

    private bool isSelectable;

    private void Awake()
    {
        this.spaceObject = GetComponent<Transform>();
        this.componentRenderer = GetComponent<Renderer>();
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

    public LinePosition[] GetLinePositions()
    {
        return (LinePosition[]) lines.Clone();
    }

    private void UpdateColour()
    {
        componentRenderer.material.color = isSelectable ? Color.green : Color.black;
    }

    public override string ToString()
    {
        return "Space(" + transform.position.x + "," + transform.position.z + ")";
    }
}
