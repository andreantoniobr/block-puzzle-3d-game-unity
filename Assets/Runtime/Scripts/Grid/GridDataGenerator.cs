using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDataGenerator : MonoBehaviour
{
    [SerializeField] private int gridSizeX = 3;
    [SerializeField] private int gridSizeY = 3;

    private GridCell[,] gridCellData;

    public GridCell[,] GetGridCellData()
    {
        StartGridCellData();
        GenerateGridCellData();
        return gridCellData;
    }

    private void StartGridCellData()
    {
        gridCellData = new GridCell[gridSizeX, gridSizeY];
    }

    private void GenerateGridCellData()
    {
        for (int x = 0; x < gridSizeX; x++)
        {
            for (int y = 0; y < gridSizeY; y++)
            {
                gridCellData[x, y] = new GridCell
                {
                    Position = new Vector2Int(x, y)
                };
            }
        }
    }
}
