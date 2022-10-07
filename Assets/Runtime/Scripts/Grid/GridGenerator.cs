using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;

    [SerializeField] private GridCell gridCellModel;
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private float positionZ = 0;
    [SerializeField] private Vector2 gridCellPositionDecrement;
    
    private int layerMaskValue;
      
    private GridCell[,] gridCellData;
    
    public Vector2Int GridSize => gridSize;
    public Vector2 GridCellPositionDecrement => gridCellPositionDecrement;
    public GridCell[,] GridCellData => gridCellData;

    private void Awake()
    {
        StartGridCellData();
        GenerateGrid();
    }

    private void StartGridCellData()
    {
        gridCellData = new GridCell[gridSize.x, gridSize.y];
    }

    private void GenerateGrid()
    {
        if (gridCellData.Length > 0)
        {
            gridCellPositionDecrement.x = GetDecrement(gridSize.x);
            gridCellPositionDecrement.y = GetDecrement(gridSize.y);

            for (int x = 0; x < gridSize.x; x++)
            {
                for (int y = 0; y < gridSize.y; y++)
                {
                    SetGridCellInstance(x, y);
                }
            }
        }
    }

    private void SetGridCellInstance(int x, int y)
    {
        Vector3 gridCellPosition = GetGridCellPosition(x, y);
        GridCell currentGridCell = Instantiate(gridCellModel, gridCellPosition, Quaternion.identity, transform);
        if (currentGridCell)
        {
            gridCellData[x, y] = currentGridCell;
            gridCellData[x, y].Position = new Vector2Int(x, y);
            currentGridCell.name = $"GridCell[{x},{y}]";
        }
    }

    private Vector3 GetGridCellPosition(int x, int y)
    {
        float gridCellPositionX = (x - gridCellPositionDecrement.x) * gridCellSize;
        float gridCellPositionY = (y - gridCellPositionDecrement.y) * gridCellSize;
        return new Vector3(gridCellPositionX, gridCellPositionY, positionZ);
    }

    private float GetDecrement(int a)
    {
        float b = (a / 2) - 0.5f;
        if (a % 2 != 0)
        {
            b = (a - 1) / 2;
        }
        return b;
    }    
}
