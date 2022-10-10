using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Block2
{
    public List<GridCell> GridCells;
}

[RequireComponent(typeof(GridGenerator))]
public class GridCellController : MonoBehaviour
{    
    [SerializeField] private GridCell selectedGridCell;
    [SerializeField] private BlockData blockData;

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
        BlocksController.SelectedMainBlockEvent += OnSelectedMainBlock;
    }

    private void UnsubscribeInEvents()
    {
        GridCellSelector.GridCellHoverEvent -= OnHoverGridCell;
        GridCellSelector.GridCellSelectedEvent -= OnSelectGridCell;
        BlocksController.SelectedMainBlockEvent -= OnSelectedMainBlock;
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
            TryPlaceBlock();
        }
    }
    
    private void OnSelectedMainBlock(Block block)
    {
        if (block)
        {
            blockData = block.BlockData;
        }        
    }

    private void TryPlaceBlock()
    {
        if (selectedGridCell && CanGetSelectedGridCells(selectedGridCell, out List<GridCell> gridCells))
        {
            if (IsEmptyGridCells(gridCells))
            {
                PlaceBlock(gridCells);
                CheckAllCompleteGridParts();
            }
        }
    }

    private void PlaceBlock(List<GridCell> gridCells)
    {
        SetFullGridCells(gridCells);
    }

    private void CheckAllCompleteGridParts()
    {
    }

    private void RemoveCompleteGridPart()
    {

    }

    

    private bool IsPlaceableBlock()
    {
        bool isPlaceableBlock = false;
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
                                isPlaceableBlock = true;
                                break;
                            }
                        }
                    }
                }
            }
        }
        return isPlaceableBlock;
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
        if (selectedGridCell && blockData && gridCellData.Length > 0)
        {
            int blockRowsAmount = BlockDataConstants.RowsAmount;
            int blockCollsAmount = BlockDataConstants.CollsAmount;
            int startX = selectedGridCell.Position.x - (int) ((blockRowsAmount - 1) / 2);
            int startY = selectedGridCell.Position.y - (int) ((blockCollsAmount - 1) / 2);
            for (int x = 0; x < blockRowsAmount; x++)
            {
                for (int y = 0; y < blockCollsAmount; y++)
                {
                    if (IsFullBlockCell(x, y))
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

    private bool IsFullBlockCell(int x, int y)
    {
        return blockData.BlockCells[x + y * BlockDataConstants.RowsAmount];
    }

    private bool IsInArrayRange(int index, int maxIndexInRange)
    {
        return index >= 0 && index <= maxIndexInRange;
    }
}
