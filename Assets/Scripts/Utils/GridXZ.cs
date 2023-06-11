using System;
using UnityEngine;

public class GridXZ<TGridObject>
{
    public event EventHandler<OnGridValueChangedEventArgs> OnGridObjectChanged;
    public class OnGridValueChangedEventArgs : EventArgs
    {
        public int x;
        public int z;
    }

    private int width;
    private int height;
    private float cellSize;
    private TGridObject[,] gridArray;
    private Vector3 originPosition;

    /**
     * @param width the width of the grid (i.e. the number of cells along the x-axis of the grid)
     * @param height the height of the grid (i.e. the number of cells along the z-axis of the grid).
     * @param cellSize the size of each cell in the grid
     * @param originPosition the bottom left corner of the grid in Vector3 coordinates
     * @param createGridObject the function used to create the starting object for each cell. This grid is passed as the first parameter, 
     *        along with the x and y ints for each cell as it iterates over the grid.
     * @param showDebug whether to draw the grid in the world for debug purposes
     */
    public GridXZ(int width, int height, float cellSize, Vector3 originPosition, Func<GridXZ<TGridObject>, int, int, TGridObject> createGridObject, bool showDebug = false)
    {
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;
        this.originPosition = originPosition;

        gridArray = new TGridObject[width, height];

        for (int x = 0; x < gridArray.GetLength(0); x++)
        {
            for (int z = 0; z < gridArray.GetLength(1); z++)
            {
                gridArray[x, z] = createGridObject(this, x, z);
            }
        }

        if (showDebug)
        {
            TextMesh[,] debugTextArray = new TextMesh[width, height];

            for (int x = 0; x < gridArray.GetLength(0); x++)
            {
                for (int z = 0; z < gridArray.GetLength(1); z++)
                {
                    debugTextArray[x, z] = Utils.CreateWorldText(gridArray[x, z]?.ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize, 0f, cellSize) * 0.5f, 20, Color.white, TextAnchor.MiddleCenter);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), Color.white, 100f);
                    Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), Color.white, 100f);
                }
            }
            Debug.DrawLine(GetWorldPosition(0, height), GetWorldPosition(width, height), Color.white, 100f);
            Debug.DrawLine(GetWorldPosition(width, 0), GetWorldPosition(width, height), Color.white, 100f);

            OnGridObjectChanged += (object sender, OnGridValueChangedEventArgs args) =>
            {
                debugTextArray[args.x, args.z].text = gridArray[args.x, args.z]?.ToString();
            };
        }
    }

    public int GetWidth()
    {
        return width;
    }

    public int GetHeight()
    {
        return height;
    }

    public float GetCellSize()
    {
        return cellSize;
    }

    /**
     * Takes a grid position in terms of x and z and returns the Vector3 of its position in the world space.
     */
    public Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0f, z) * cellSize + originPosition;
    }

    /**
     * Takes a Vector3 of a position in the world space and converts it into x and z coordinates for which
     * cell in the grid that position corresponds to.
     */
    public void GetXY(Vector3 worldPosition, out int x, out int z)
    {
        x = Mathf.FloorToInt((worldPosition - originPosition).x / cellSize);
        z = Mathf.FloorToInt((worldPosition - originPosition).z / cellSize);
    }

    /**
     * @param x the cell coordinate on the x-axis of the grid
     * @param y the cell coordinate on the y-axis of the grid
     * @param value the grid object to be placed in the grid cell
     */
    public void SetGridObject(int x, int z, TGridObject value)
    {
        if (x >= 0 && z >= 0 && x < width && z < height)
        {
            gridArray[x, z] = value;
            if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });
        }
    }

    /**
     * @param worldPosition the cell position in the world
     * @param value the grid object to be placed in the grid cell
     */
    public void SetGridObject(Vector3 worldPosition, TGridObject value)
    {
        int x, z;
        GetXY(worldPosition, out x, out z);
        SetGridObject(x, z, value);
    }

    /**
     * @param x the cell coordinate on the x-axis of the grid
     * @param z the cell coordinate on the z-axis of the grid
     */
    public void TriggerGridObjectChanged(int x, int z)
    {
        if (OnGridObjectChanged != null) OnGridObjectChanged(this, new OnGridValueChangedEventArgs { x = x, z = z });
    }

    /**
     * @param x the cell coordinate on the x-axis of the grid
     * @param z the cell coordinate on the z-axis of the grid
     */
    public TGridObject GetGridObject(int x, int z)
    {
        if (x >= 0 && x < width && z >= 0 && z < height)
        {
            return gridArray[x, z];
        }
        else
        {
            return default(TGridObject);
        }
    }

    /**
     * @param worldPosition the cell position in the world
     */
    public TGridObject GetGridObject(Vector3 worldPosition)
    {
        int x, z;
        GetXY(worldPosition, out x, out z);
        return GetGridObject(x, z);
    }
}
