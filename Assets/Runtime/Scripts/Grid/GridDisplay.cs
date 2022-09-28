using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridDisplay : MonoBehaviour
{
    [SerializeField] private GridCellModel gridCellModel;
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private float gridPositionZ = 0;
    [SerializeField] private LayerMask layerMask;

    private int layerMaskValue;
    private float gridCellPositionDecrementX;
    private float gridCellPositionDecrementY;

    private void Awake()
    {
        layerMaskValue = LayerMaskUtility.GetLayerMask(layerMask);
    }

    public void Display(GridCell[,] gridCellData)
    {
        if (gridCellData.Length > 0)
        {
            int gridSizeX = gridCellData.GetLength(0);
            int gridSizeY = gridCellData.GetLength(1);

            gridCellPositionDecrementX = GetDecrement(gridSizeX);
            gridCellPositionDecrementY = GetDecrement(gridSizeY);

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    GetGridCellModelInstance(x, y, gridCellData);
                }
            }
        }
    }

    private void GetGridCellModelInstance(int x, int y, GridCell[,] gridCellData)
    {
        Vector3 gridCellPosition = GetGridCellPosition(x, y);
        GridCellModel currentGridCellModel = Instantiate(gridCellModel, gridCellPosition, Quaternion.identity, transform);
        if (currentGridCellModel)
        {
            currentGridCellModel.name = $"GridCell[{x},{y}]";
            currentGridCellModel.gameObject.layer = layerMaskValue;
            currentGridCellModel.GridCell = gridCellData[x, y];
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
