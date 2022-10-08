using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class GridCellController : MonoBehaviour
{    
    [SerializeField] private GridCell selectedGridCell;
    [SerializeField] private GridPartData gridPartData;

    private GridGenerator gridGenerator;

    private int gridSizeX;
    private int gridSizeY;

    private int maxGridCellX;
    private int maxGridCellY;

    private GridCell[,] gridCellData;

    private List<GridCell> hoverGridCells;

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

        hoverGridCells = new List<GridCell>();

        SubscribeInEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeInEvents();
    }

    private void SubscribeInEvents()
    {
        GridCellSelector.GridCellHoverEvent += OnHoverGridCell;
        GridCellSelector.GridCellSelectedEvent += OnSelectGridCell;
        PartsSelectorController.SelectedMainPartEvent += OnSelectedMainPart;
    }

    private void UnsubscribeInEvents()
    {
        GridCellSelector.GridCellHoverEvent -= OnHoverGridCell;
        GridCellSelector.GridCellSelectedEvent -= OnSelectGridCell;
        PartsSelectorController.SelectedMainPartEvent -= OnSelectedMainPart;
    }

    private void OnHoverGridCell(GridCell gridCell)
    {        
        if (gridCell)
        {
            EraseHoverGridCells(hoverGridCells);
            if (CanGetSelectedGridCells(gridCell, out List<GridCell> gridCells))
            {
                if (IsEmptyGridCells(gridCells))
                {                    
                    SetHoverGridCells(gridCells);
                }
            }
        }
    }

    private void OnSelectGridCell(GridCell gridCell)
    {
        if (gridCell && selectedGridCell != gridCell)
        {
            selectedGridCell = gridCell;
            SetGridPartInGridCells();
        }
    }
    
    private void OnSelectedMainPart(Part part)
    {
        if (part)
        {
            gridPartData = part.GridPartData;
        }        
    }

    private void SetGridPartInGridCells()
    {
        if (selectedGridCell)
        {
            if (CanGetSelectedGridCells(selectedGridCell, out List<GridCell> gridCells))
            {
                if (IsEmptyGridCells(gridCells))
                {
                    SetFullGridCells(gridCells);
                }
            }
        }
    }

    private bool IsPlaceableGridPart()
    {
        bool isPlaceableGridPart = false;
        if (gridCellData.Length > 0)
        {
            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    GridCell gridCell = gridCellData[x,y];
                    if (gridCell)
                    {
                        if (CanGetSelectedGridCells(gridCell, out List<GridCell> gridCells))
                        {
                            if (IsEmptyGridCells(gridCells))
                            {
                                isPlaceableGridPart = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        return isPlaceableGridPart;
    }

    private bool IsEmptyGridCells(List<GridCell> gridCells)
    {
        bool isEmptyGridCells = true;
        if (gridCells.Count > 0)
        {
            for (int i = 0; i < gridCells.Count; i++)
            {
                GridCell currentGridCell = gridCells[i];
                if (currentGridCell && currentGridCell.IsFull == true)
                {
                    isEmptyGridCells = false;
                    break;
                }
            }
        }
        return isEmptyGridCells;
    }

    private void SetFullGridCells(List<GridCell> gridCells)
    {
        if (gridCells.Count > 0)
        {
            for (int i = 0; i < gridCells.Count; i++)
            {
                GridCell currentGridCell = gridCells[i];
                if (currentGridCell)
                {
                    currentGridCell.IsFull = true;
                }
            }
        }
    }

    private void SetHoverGridCells(List<GridCell> gridCells)
    {        
        if (gridCells.Count > 0)
        {
            for (int i = 0; i < gridCells.Count; i++)
            {
                GridCell currentGridCell = gridCells[i];
                if (currentGridCell)
                {
                    currentGridCell.IsHover = true;
                    hoverGridCells.Add(currentGridCell);
                }
            }
        }
    }

    private void EraseHoverGridCells(List<GridCell> gridCells)
    {
        if (gridCells.Count > 0)
        {
            for (int i = 0; i < gridCells.Count; i++)
            {
                GridCell currentGridCell = gridCells[i];
                if (currentGridCell)
                {
                    currentGridCell.IsHover = false;
                }
            }
            gridCells.Clear();
        }        
    }

    private bool CanGetSelectedGridCells(GridCell selectedGridCell, out List<GridCell> gridCells)
    {
        bool canGetSelectedGridCells = true;
        gridCells = new List<GridCell>();
        if (selectedGridCell && gridPartData && gridCellData.Length > 0)
        {
            int gridPartRowsAmount = GridPartDataConstants.RowsAmount;
            int gridPartCollsAmount = GridPartDataConstants.CollsAmount;
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
                            gridCells.Add(gridCellData[gridCellX, gridCellY]);
                        }
                        else
                        {
                            canGetSelectedGridCells = false;
                            break;
                        }
                    }
                }
            }
        }
        return canGetSelectedGridCells;
    }

    private bool IsFullGridPartCell(int x, int y)
    {
        return gridPartData.GridPart[x + y * GridPartDataConstants.RowsAmount];
    }

    private bool IsInArrayRange(int index, int maxIndexInRange)
    {
        return index >= 0 && index <= maxIndexInRange;
    }
}
