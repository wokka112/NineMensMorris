using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Space[] spaces = new Space[2];

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        Vector3 startPosition = spaces[0].GetPosition();
        startPosition.y = 0.1f;

        Vector3 endPosition = spaces[1].GetPosition();
        endPosition.y = 0.1f;

        lineRenderer.SetPositions(new Vector3[] { startPosition, endPosition });
    }
}
