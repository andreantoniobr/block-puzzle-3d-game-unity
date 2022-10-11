using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class GridBackgroundGenerator : MonoBehaviour
{
    [SerializeField] private GridBackgroundCell gridBackgroundCellModel;
    [SerializeField] private float gridCellSize = 1f;
    [SerializeField] private float gridCellPositionZ = 0;
    [SerializeField] private LayerMask layerMask;

    private GridGenerator gridGenerator;
    private int layerMaskValue;

    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
        GetLayerMaskValue();
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GetLayerMaskValue()
    {
        layerMaskValue = MathHelper.GetIntLayerMaskValue(layerMask);
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
        Vector3 gridCellPosition = MathHelper.GetGridCellPosition(x, y, gridGenerator.GridSize, gridCellSize, gridCellPositionZ);
        GridBackgroundCell gridBrackgroundCell = Instantiate(gridBackgroundCellModel, gridCellPosition, Quaternion.identity, transform);
        if (gridBrackgroundCell)
        {
            gridBrackgroundCell.name = $"GridBrackgroundCell[{x},{y}]";
            gridBrackgroundCell.gameObject.layer = layerMaskValue;
            SetGriCell(x, y, gridBrackgroundCell);
        }
    }

    private void SetGriCell(int x, int y, GridBackgroundCell gridBrackgroundCell)
    {
        GridCell gridCell = gridGenerator.GridCellData[x, y];
        if (gridCell)
        {
            gridBrackgroundCell.GridCell = gridCell;
        }
    }
}
