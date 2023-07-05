using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState
{
    private Space[] allSpaces;
    private Dictionary<LinePosition, Space[]> lines;
    private LayerMask spaceLayer;
    private LayerMask pieceLayer;

    public BoardState(Space[] allSpaces, LayerMask spaceLayer, LayerMask pieceLayer)
    {
        this.allSpaces = allSpaces;
        this.spaceLayer = spaceLayer;
        this.pieceLayer = pieceLayer;

        lines = new Dictionary<LinePosition, Space[]>();
        foreach (Space space in allSpaces) {
            foreach(LinePosition linePosition in space.GetLinePositions())
            {
                if (!lines.TryGetValue(linePosition, out Space[] lineSpaces))
                {
                    lineSpaces = new Space[3];
                    lineSpaces[0] = space;
                    lines.Add(linePosition, lineSpaces);
                } else if (lineSpaces[1] == null)
                {
                    lineSpaces[1] = space;
                } else if (lineSpaces[2] == null)
                {
                    lineSpaces[2] = space;
                } else
                {
                    Debug.LogError("Spaces already full for line position: " + linePosition + ". Not adding this space to line position: " + space);
                }
            }
        }
    }

    private void LogLines()
    {
        foreach (LinePosition linePosition in lines.Keys)
        {
            Debug.Log("Getting lines for " + linePosition);
            lines.TryGetValue(linePosition, out Space[] lineSpaces);
            for (int i = 0; i < lineSpaces.Length; i++)
            {
                Debug.Log("Space " + i + ": " + lineSpaces[i]);
            }
        }
    }

    public bool PieceMadeAMill(Piece piece)
    {
        Space space = piece.GetSpace();
        foreach (LinePosition linePosition in space.GetLinePositions())
        {
            if (LineFilledBySameColour(linePosition))
            {
                return true;
            }
        }

        return false;
    }

    public bool LineFilledBySameColour(LinePosition linePosition)
    {
        lines.TryGetValue(linePosition, out Space[] lineSpaces);
        if (lineSpaces[0].IsEmpty())
        {
            return false;
        }

        Piece.PieceColour colour = lineSpaces[0].GetPiece().GetColour();
        for (int i = 1; i < lineSpaces.Length; i++)
        {
            if (lineSpaces[i].IsEmpty())
            { 
                return false;
            }

            if (lineSpaces[i].GetPiece().GetColour() != colour)
            {
                return false;
            }
        }

        return true;
    }

    public void MakeAllEmptySpacesSelectable()
    {
        foreach (Space space in allSpaces)
        {
            if (space.IsEmpty())
            {
                space.SetSelectable();
            }
        }
    }

    public void MakeAllSpacesUnselectable()
    {
        foreach (Space space in allSpaces)
        {
            space.SetUnselectable();
        }
    }

    public Space GetSpaceClicked(Vector3 mousePosition)
    {
        Space space = null;
        RaycastHit hitData;

        Ray ray = Camera.main.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out hitData, 1000, spaceLayer))
        {
            Debug.Log("Hit object: " + hitData.transform.gameObject.name);
            Debug.Log("Returning space: " + space.ToString());
            space = hitData.transform.GetComponent<Space>();
        }
        else
        {
            Debug.Log("Didn't hit object!");
        }

        return space;
    }
}
