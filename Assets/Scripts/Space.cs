using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Space : MonoBehaviour
{
    [SerializeField]
    private List<Space> neighbours;

    private Transform spaceObject;
    private Piece piece;

    private void Start()
    {
        this.spaceObject = GetComponent<Transform>();
    }
}
