using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField]
    private List<Space> neighbours;

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

    private void UpdateColour()
    {
        Debug.Log("Material colour updated to white");
        renderer.material.color = Color.white;
    }
}
