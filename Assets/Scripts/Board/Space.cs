using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    //TODO go through each space and assign neighbours and line positions

    [SerializeField]
    private List<Space> neighbours;
    [SerializeField]
    private LinePosition[] linePositions = new LinePosition[2];

    private Transform spaceObject;
    private Renderer renderer;
    private Piece piece;

    private bool isSelectable;

    private void Awake()
    {
        this.spaceObject = GetComponent<Transform>();
        this.renderer = GetComponent<Renderer>();
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
        return (LinePosition[]) linePositions.Clone();
    }

    private void UpdateColour()
    {
        Debug.Log("Material colour updated");
        renderer.material.color = isSelectable ? Color.green : Color.black;
    }

    public override string ToString()
    {
        return "Space(" + transform.position.x + "," + transform.position.z + ")";
    }
}
