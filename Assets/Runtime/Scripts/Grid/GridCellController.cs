using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GridPart
{
    public List<GridCell> GridCells;
}

[RequireComponent(typeof(GridGenerator))]
public class GridCellController : MonoBehaviour
{
    [SerializeField] private Vector2Int gridChunkSize;
    [SerializeField] private GridCell selectedGridCell;
    [SerializeField] private BlockData blockData;
    [SerializeField] private List<GridPart> completeGridParts = new List<GridPart>();

    private GridGenerator gridGenerator;

    private int gridSizeX;
    private int gridSizeY;

    private GridCell[,] gridCellData;
    private List<GridCell> hoverGridCells = new List<GridCell>();

    [SerializeField] private int gridChunksAmount;

    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
        SetGridCellData();
        SetGridSize();
        SetGridChunksAmount();
        SubscribeInEvents();
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

    private void SetGridChunksAmount()
    {
        gridChunksAmount = MathHelper.GetGridsAmountInMainGrid(gridChunkSize.x, gridChunkSize.y, gridSizeX, gridSizeY);
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
        completeGridParts.Clear();
        List<GridPart> rows = GetCompleteRows();
        List<GridPart> colls = GetCompleteColls();
        completeGridParts.AddRange(rows);
        completeGridParts.AddRange(colls);
        //completeGridParts;
        //pega rows completas
        //pega colls completas
        //pega chunks completos
    }

    private List<GridPart> GetCompleteColls()
    {
        List<GridPart> gridParts = new List<GridPart>();        
        for (int x = 0; x < gridSizeX; x++)
        {
            GridPart currentGridPart = new GridPart
            {
                GridCells = new List<GridCell>()
            };
            for (int y = 0; y < gridSizeY; y++)
            {
                GridCell gridCell = gridCellData[x, y];
                if (gridCell && gridCell.IsFull)
                {
                    currentGridPart.GridCells.Add(gridCell);
                }
                else
                {
                    currentGridPart.GridCells.Clear();
                    break;
                }
            }
            CheckIsCompleteGridPart(gridParts, currentGridPart, gridSizeX);
        }
        return gridParts;
    }    

    private List<GridPart> GetCompleteRows()
    {
        List<GridPart> gridParts = new List<GridPart>();
        for (int y = 0; y < gridSizeY; y++)
        {
            GridPart currentGridPart = new GridPart
            {
                GridCells = new List<GridCell>()
            };
            for (int x = 0; x < gridSizeX; x++)
            {
                GridCell gridCell = gridCellData[x, y];
                if (gridCell && gridCell.IsFull)
                {
                    currentGridPart.GridCells.Add(gridCell);
                }
                else
                {
                    currentGridPart.GridCells.Clear();
                    break;
                }
            }
            CheckIsCompleteGridPart(gridParts, currentGridPart, gridSizeY);
        }
        return gridParts;
    }

    private void CheckIsCompleteGridPart(List<GridPart> gridParts, GridPart gridPart, int gridPartSize)
    {
        int gridCellsAmount = gridPart.GridCells.Count;
        if (gridCellsAmount > 0 && gridCellsAmount >= gridPartSize)
        {
            gridParts.Add(gridPart);
        }
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
                GridCell gridCell = gridCells[i];
                if (gridCell)
                {
                    gridCell.IsFull = true;
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
                    if (IsFullBlockCell(x, y))
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

    private bool IsFullBlockCell(int x, int y)
    {
        return blockData.BlockCells[x + y * BlockDataConstants.RowsAmount];
    }    
}
