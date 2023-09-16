using UnityEngine;

public class Line : MonoBehaviour
{
    private LineRenderer lineRenderer;
    [SerializeField]
    private Space[] spaces = new Space[3];

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.startWidth = 0.2f;
        lineRenderer.endWidth = 0.2f;

        Vector3 startPosition = spaces[0].GetPosition();
        startPosition.y = 0.1f;

        Vector3 endPosition = spaces[spaces.Length - 1].GetPosition();
        endPosition.y = 0.1f;

        lineRenderer.SetPositions(new Vector3[] { startPosition, endPosition });

        foreach (Space space in spaces)
        {
            space.AddLine(this);
        }
    }

    public bool IsAMill()
    {
        Space firstSpace = spaces[0];
        if (firstSpace.IsEmpty())
        {
            return false;
        }

        Colour colour = firstSpace.GetPiece().GetColour();

        for(int i = 1; i< spaces.Length; i++)
        {
            Space space = spaces[i];
            if (space.IsEmpty())
            {
                return false;
            }

            if (space.GetPiece().GetColour() != colour)
            {
                return false;
            }
        }

        return true;
    }
}
