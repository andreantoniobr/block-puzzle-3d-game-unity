using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class GridPartsController : MonoBehaviour
{
    [SerializeField] private List<GridPart> completeGridParts = new List<GridPart>();
    [SerializeField] private int gridChunksAmount;

    private int gridSizeX;
    private int gridSizeY;
    private GridCell[,] gridCellData;
    private GridGenerator gridGenerator;

    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
    }

    private void Start()
    {        
        SetGridCellData();
        SetGridSize();
        SetGridChunksAmount();
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
        //gridChunksAmount = MathHelper.GetGridsAmountInMainGrid(gridChunkSize.x, gridChunkSize.y, gridSizeX, gridSizeY);
    }

    public void GetCompleteGridParts(int chunkSizeX, int chunkSizeY)
    {
        completeGridParts.Clear();
        List<GridPart> rows = GetCompleteRows();
        List<GridPart> colls = GetCompleteColls();
        List<GridPart> chunks = GetCompleteChunks(chunkSizeX, chunkSizeY);

        completeGridParts.AddRange(rows);
        completeGridParts.AddRange(colls);
        completeGridParts.AddRange(chunks);
        //completeGridParts;
        //pega rows completas
        //pega colls completas
        //pega chunks completos
    }

    private List<GridPart> GetCompleteChunks(int chunkSizeX, int chunkSizeY)
    {
        List<GridPart> gridParts = new List<GridPart>();
        int chunksAmountX = MathHelper.GetGridsAmountInSideOfMainGrid(chunkSizeX, gridSizeX);
        int chunksAmountY = MathHelper.GetGridsAmountInSideOfMainGrid(chunkSizeY, gridSizeY);
        int chunkSize = chunkSizeX * chunkSizeY;
        if (chunksAmountX > 0 && chunksAmountY > 0)
        {
            for (int x = 0; x < chunksAmountX; x++)
            {
                for (int y = 0; y < chunksAmountY; y++)
                {
                    int chunkX = x * chunkSizeX;
                    int chunkY = y * chunkSizeY;
                    //Debug.Log($"{chunkX}, {chunkY}");
                    GridPart gridPart = GetChunk(chunkX, chunkY, chunkSizeX, chunkSizeY);                    
                    CheckIsCompleteGridPart(gridParts, gridPart, chunkSize);
                }
            }            
        }        
        return gridParts;
    }

    
    private GridPart GetChunk(int chunkX, int chunkY, int chunkSizeX, int chunkSizeY)
    {
        GridPart gridPart = GetNewGridPart();
        for (int x = 0; x < chunkSizeX; x++)
        {
            for (int y = 0; y < chunkSizeY; y++)
            {
                int gridCellX = x + chunkX;
                int gridCellY = y + chunkY;

                GridCell gridCell = gridCellData[gridCellX, gridCellY];                
                if (!TryAddGridCellInGridPart(gridCell, gridPart))
                {
                    break;
                }
            }
        }
        return gridPart;
    }

    private List<GridPart> GetCompleteColls()
    {
        List<GridPart> gridParts = new List<GridPart>();
        for (int x = 0; x < gridSizeX; x++)
        {
            GridPart gridPart = GetNewGridPart();
            if (gridPart != null)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    GridCell gridCell = gridCellData[x, y];
                    if (!TryAddGridCellInGridPart(gridCell, gridPart))
                    {
                        break;
                    }
                }
                CheckIsCompleteGridPart(gridParts, gridPart, gridSizeX);
            }

        }
        return gridParts;
    }

    private List<GridPart> GetCompleteRows()
    {
        List<GridPart> gridParts = new List<GridPart>();
        for (int y = 0; y < gridSizeY; y++)
        {
            GridPart gridPart = GetNewGridPart();
            for (int x = 0; x < gridSizeX; x++)
            {
                GridCell gridCell = gridCellData[x, y];
                if (!TryAddGridCellInGridPart(gridCell, gridPart))
                {
                    break;
                }
            }
            CheckIsCompleteGridPart(gridParts, gridPart, gridSizeY);
        }
        return gridParts;
    }

    private bool TryAddGridCellInGridPart(GridCell gridCell, GridPart gridPart)
    {
        bool canAddGridCellInGriPart = false;
        if (gridCell && gridCell.IsFull)
        {
            canAddGridCellInGriPart = true;
            gridPart.GridCells.Add(gridCell);
        }
        else
        {
            gridPart.GridCells.Clear();
        }
        return canAddGridCellInGriPart;
    }

    private GridPart GetNewGridPart()
    {
        return new GridPart
        {
            GridCells = new List<GridCell>()
        };
    }

    private void CheckIsCompleteGridPart(List<GridPart> gridParts, GridPart gridPart, int gridPartSize)
    {
        int gridCellsAmount = gridPart.GridCells.Count;
        if (gridCellsAmount > 0 && gridCellsAmount >= gridPartSize)
        {
            gridParts.Add(gridPart);
        }
    }
}
