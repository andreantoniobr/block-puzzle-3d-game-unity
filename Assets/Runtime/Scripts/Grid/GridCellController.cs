using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class GridCellController : MonoBehaviour
{    
    [SerializeField] private GridCell selectedGridCell;
    [SerializeField] private GridPartsData gridPartsData;

    private GridGenerator gridGenerator;

    private int gridSizeX;
    private int gridSizeY;

    private int maxGridCellX;
    private int maxGridCellY;

    private GridCell[,] gridCellData;

    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();

        if (gridGenerator)
        {
            gridCellData = gridGenerator.GridCellData;
        }

        gridSizeX = gridCellData.GetLength(0);
        gridSizeY = gridCellData.GetLength(0);

        maxGridCellX = gridSizeX - 1;
        maxGridCellY = gridSizeY - 1;

        SubscribeInEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeInEvents();
    }

    private void SubscribeInEvents()
    {
        GridCellSelector.GridCellSelectedEvent += OnGridCellSelected;
    }

    private void UnsubscribeInEvents()
    {
        GridCellSelector.GridCellSelectedEvent -= OnGridCellSelected;
    }

    private void OnGridCellSelected(GridCell gridCell)
    {
        if (gridCell && selectedGridCell != gridCell)
        {
            selectedGridCell = gridCell;
            SetGridPartInGridCells(gridCell);
        }        
    }

    
    private void SetGridPartInGridCells(GridCell selectedGridCell)
    {
        if (selectedGridCell)
        {
            List<GridCell> currentSelectedGridCells = GetSelectedGridCells(selectedGridCell);
            if (currentSelectedGridCells.Count > 0)
            {
                for (int i = 0; i < currentSelectedGridCells.Count; i++)
                {
                    GridCell currentGridCell = currentSelectedGridCells[i];
                    if (currentGridCell)
                    {
                        currentGridCell.IsFull = true;
                    }                    
                }
            }
        }             
    }

    private List<GridCell> GetSelectedGridCells(GridCell selectedGridCell)
    {
        List<GridCell> selectedGridCells = new List<GridCell>();        
        if (selectedGridCell && gridPartsData && gridCellData.Length > 0)
        {
            int gridPartRowsAmount = GridPartsDataConstants.RowsAmount;
            int gridPartCollsAmount = GridPartsDataConstants.CollsAmount;
            int startX = selectedGridCell.Position.x - (int) ((gridPartRowsAmount - 1) / 2);
            int startY = selectedGridCell.Position.y - (int) ((gridPartCollsAmount - 1) / 2);
            for (int x = 0; x < gridPartRowsAmount; x++)
            {
                for (int y = 0; y < gridPartCollsAmount; y++)
                {
                    if (IsFullGridPartCell(x, y))
                    {
                        int gridCellX = x + startX;
                        int gridCellY = y + startY;
                        if (IsInArrayRange(gridCellX, maxGridCellX) && IsInArrayRange(gridCellY, maxGridCellY))
                        {
                            selectedGridCells.Add(gridCellData[gridCellX, gridCellY]);
                        }
                    }
                }
            }
        }
        return selectedGridCells;
    }

    private bool IsPlaceableGridCell()
    {
        return false;
    }

    private bool IsFullGridPartCell(int x, int y)
    {
        return gridPartsData.GridParts[x + y * GridPartsDataConstants.RowsAmount];
    }

    private bool IsInArrayRange(int index, int maxIndexInRange)
    {
        return index >= 0 && index <= maxIndexInRange;
    }
}
