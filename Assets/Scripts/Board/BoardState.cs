using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardState
{
    private Space[] spaces;
    private Dictionary<LinePosition, Space[]> lines;

    public BoardState(Space[] spaces)
    {
        this.spaces = spaces;
        lines = new Dictionary<LinePosition, Space[]>();
        foreach (LinePosition linePosition in System.Enum.GetValues(typeof(LinePosition)))
        {
            lines.Add(linePosition, new Space[3]);
        }

        foreach (Space space in spaces) {
            foreach(LinePosition linePosition in space.GetLinePositions())
            {
                lines.TryGetValue(linePosition, out Space[] lineSpaces);

                if (lineSpaces[0] == null)
                {
                    lineSpaces[0] = space;
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

    public bool PieceMadeThreeInARow(Piece piece)
    {
        Space space = piece.GetSpace();
        foreach (LinePosition linePosition in space.GetLinePositions())
        {
            if (LineFilledBySameColour(linePosition))
                return true;
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
        for (int i = 1; i < spaces.Length; i++)
        {
            if (spaces[i].IsEmpty())
            {
                return false;
            }

            if (spaces[i].GetPiece().GetColour() != colour)
            {
                return false;
            }
        }

        return true;
    }
}
