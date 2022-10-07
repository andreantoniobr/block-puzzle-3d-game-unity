using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class GridBackgroundGenerator : MonoBehaviour
{
    [SerializeField] private GridBackgroundCell gridBackgroundCellModel;
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private float positionZ = 0;
    [SerializeField] private LayerMask layerMask;

    private GridGenerator gridGenerator;
    private int layerMaskValue;

    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
        SetLayerMask();
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void SetLayerMask()
    {
        layerMaskValue = LayerMaskUtility.GetLayerMask(layerMask);
    }

    private void GenerateGrid()
    {
        if (gridGenerator && gridGenerator.GridCellData.Length > 0)
        {
            for (int x = 0; x < gridGenerator.GridSize.x; x++)
            {
                for (int y = 0; y < gridGenerator.GridSize.y; y++)
                {
                    SetGridCellInstance(x, y);
                }
            }
        }
    }

    private void SetGridCellInstance(int x, int y)
    {
        Vector3 gridCellPosition = GetGridCellPosition(x, y);
        GridBackgroundCell gridBrackgroundCell = Instantiate(gridBackgroundCellModel, gridCellPosition, Quaternion.identity, transform);
        if (gridBrackgroundCell)
        {
            gridBrackgroundCell.name = $"GridBrackgroundCell[{x},{y}]";            
            gridBrackgroundCell.gameObject.layer = layerMaskValue;
            GridCell gridCell = gridGenerator.GridCellData[x, y];
            if (gridCell)
            {
                gridBrackgroundCell.GridCell = gridCell;
            }
        }
    }

    private Vector3 GetGridCellPosition(int x, int y)
    {
        float gridCellPositionX = (x - gridGenerator.GridCellPositionDecrement.x) * gridCellSize;
        float gridCellPositionY = (y - gridGenerator.GridCellPositionDecrement.y) * gridCellSize;
        return new Vector3(gridCellPositionX, gridCellPositionY, positionZ);
    }
}
