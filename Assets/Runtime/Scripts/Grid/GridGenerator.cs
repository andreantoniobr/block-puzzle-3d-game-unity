using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int gridSizeX = 3;
    [SerializeField] private int gridSizeY = 3;

    [SerializeField] private GridCell gridCellModel;
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private float gridPositionZ = 0;
    [SerializeField] private LayerMask layerMask;

    private int layerMaskValue;
    private float gridCellPositionDecrementX;
    private float gridCellPositionDecrementY;
    
    private GridCell[,] gridCellData;

    public GridCell[,] GridCellData => gridCellData;

    private void Awake()
    {
        StartGridCellData();
        SetLayerMask();
    }    

    private void Start()
    {
        GenerateGrid();
    }

    private void StartGridCellData()
    {
        gridCellData = new GridCell[gridSizeX, gridSizeY];
    }

    private void SetLayerMask()
    {
        layerMaskValue = LayerMaskUtility.GetLayerMask(layerMask);
    }

    private void GenerateGrid()
    {
        if (gridCellData.Length > 0)
        {
            gridCellPositionDecrementX = GetDecrement(gridSizeX);
            gridCellPositionDecrementY = GetDecrement(gridSizeY);

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
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
            currentGridCell.name = $"GridCell[{x},{y}]";
            currentGridCell.gameObject.layer = layerMaskValue;
        }
    }

    private Vector3 GetGridCellPosition(int x, int y)
    {
        float gridCellPositionX = (x - gridCellPositionDecrementX) * gridCellSize;
        float gridCellPositionY = (y - gridCellPositionDecrementY) * gridCellSize;
        return new Vector3(gridCellPositionX, gridCellPositionY, gridPositionZ);
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
