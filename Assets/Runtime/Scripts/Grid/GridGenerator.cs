using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize;

    [SerializeField] private GridCell gridCellModel;
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private float gridCellPositionZ = 0;
    
    private int layerMaskValue;
      
    private GridCell[,] gridCellData;
    
    public Vector2Int GridSize => gridSize;
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
        Vector3 gridCellPosition = MathHelper.GetGridCellPosition(x, y, gridSize, gridCellSize, gridCellPositionZ);
        GridCell currentGridCell = Instantiate(gridCellModel, gridCellPosition, Quaternion.identity, transform);
        if (currentGridCell)
        {
            gridCellData[x, y] = currentGridCell;
            gridCellData[x, y].Position = new Vector2Int(x, y);
            currentGridCell.name = $"GridCell[{x},{y}]";
        }
    }   
}
