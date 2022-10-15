using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
[RequireComponent(typeof(GridPartsController))]
public class GridCellController : MonoBehaviour
{
    [SerializeField] private Vector2Int gridChunkSize;
    [SerializeField] private GridCell selectedGridCell;
    [SerializeField] private BlockData blockData;
    
    private int gridSizeX;
    private int gridSizeY;
    private GridCell[,] gridCellData;
    private GridGenerator gridGenerator;
    private GridPartsController gridPartsController;
    private List<GridCell> hoverGridCells = new List<GridCell>();   
    
    public static event Action PlacedBlockEvent;

    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
        gridPartsController = GetComponent<GridPartsController>();
        SubscribeInEvents();
    }

    private void Start()
    {        
        SetGridCellData();
        SetGridSize();
    }

    private void SetGridSize()
    {
        gridSizeX = gridCellData.GetLength(0);
        gridSizeY = gridCellData.GetLength(0);
    }

    private void SetGridCellData()
    {
        if (gridGenerator)
        {
            gridCellData = gridGenerator.GridCellData;
        }
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
        BlocksController.RemoveMainBlockEvent += OnRemovedMainBlock;
    }

    private void UnsubscribeInEvents()
    {
        GridCellSelector.GridCellHoverEvent -= OnHoverGridCell;
        GridCellSelector.GridCellSelectedEvent -= OnSelectGridCell;
        BlocksController.SelectedMainBlockEvent -= OnSelectedMainBlock;
        BlocksController.RemoveMainBlockEvent -= OnRemovedMainBlock;
    }

    private void OnHoverGridCell(GridCell gridCell)
    {        
        if (gridCell)
        {
            EraseHoverGridCells();
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

    private void OnRemovedMainBlock(Block block)
    {
        blockData = null;
    }

    private void TryPlaceBlock()
    {
        if (selectedGridCell && CanGetSelectedGridCells(selectedGridCell, out List<GridCell> gridCells))
        {
            if (IsEmptyGridCells(gridCells))
            {
                PlaceBlock(gridCells);
                PlacedBlockEvent?.Invoke();
                RemoveAllCompleteGridPartsParts();
            }
        }
    }

    private void CheckAllCompleteGridParts()
    {
        if (gridPartsController)
        {
            List<GridPart> completeGridParts = gridPartsController.GetCompleteGridParts(gridChunkSize.x, gridChunkSize.y);
        }
        //completeGridParts;
        //pega rows completas
        //pega colls completas
        //pega chunks completos
    }

    private void RemoveAllCompleteGridPartsParts()
    {
        List<GridPart> gridParts = gridPartsController.GetCompleteGridParts(gridChunkSize.x, gridChunkSize.y);
        int gridPartsAmount = gridParts.Count;
        if (gridPartsAmount > 0)
        {
            for (int i = 0; i < gridPartsAmount; i++)
            {
                GridPart gridPart = gridParts[i];
                if (gridPart != null && gridPart.GridCells.Count > 0)
                {
                    SetFullGridCells(gridPart.GridCells, false);
                }
            }
        }
    }

    private void PlaceBlock(List<GridCell> gridCells)
    {
        SetFullGridCells(gridCells, true);
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

    private void SetFullGridCells(List<GridCell> gridCells, bool isFull)
    {
        if (gridCells.Count > 0)
        {
            for (int i = 0; i < gridCells.Count; i++)
            {
                GridCell gridCell = gridCells[i];
                if (gridCell)
                {
                    gridCell.IsFull = isFull;
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
                GridCell gridCell = gridCells[i];
                if (gridCell)
                {
                    gridCell.IsHover = true;
                    hoverGridCells.Add(gridCell);
                }
            }
        }
    }

    private void EraseHoverGridCells()
    {
        if (hoverGridCells.Count > 0)
        {
            for (int i = 0; i < hoverGridCells.Count; i++)
            {
                GridCell gridCell = hoverGridCells[i];
                if (gridCell)
                {
                    gridCell.IsHover = false;
                }
            }
            hoverGridCells.Clear();
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
                    if (BlockDataHelper.IsFullBlockCell(x, y, blockData))
                    {
                        int gridCellX = x + startX;
                        int gridCellY = y + startY;
                        if (MathHelper.IsInArrayRange(gridCellX, gridSizeX) && MathHelper.IsInArrayRange(gridCellY, gridSizeY))
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
}
