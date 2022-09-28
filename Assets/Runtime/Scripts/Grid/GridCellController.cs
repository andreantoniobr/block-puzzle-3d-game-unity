using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class GridCellController : MonoBehaviour
{    
    [SerializeField] private GridCell selectedGridCell;
    [SerializeField] private GridPartsData gridPartsData;

    private GridGenerator gridGenerator;

    private void Awake()
    {
        gridGenerator = GetComponent<GridGenerator>();
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
        if (gridGenerator && gridCell)
        {
            selectedGridCell = gridCell;
            gridCell.IsFull = true;
        }
    }

    
    private void SetGridPartInGridCells(GridCell gridCell, GridPartsData gridPartsData)
    {
        if (gridCell && gridPartsData)
        {
            int initialX = gridCell.Position.x - (int)((GridPartsDataConstants.RowsAmount - 1) / 2);
            int initialY = gridCell.Position.y - (int)((GridPartsDataConstants.CollsAmount - 1) / 2);
            for (int x = 0; x < GridPartsDataConstants.RowsAmount; x++)
            {
                for (int y = 0; y < GridPartsDataConstants.CollsAmount; y++)
                {
                    if (gridPartsData.GridParts[x * GridPartsDataConstants.RowsAmount + y])
                    {
                        /*
                        Vector2Int gridCellDataPosition = gridCell.GridCellData.Position;
                        gridCell.SetGridCellDataState(true);
                        gridGenerator.GridCellData[gridCellDataPosition.x, gridCellDataPosition.y].IsFull = true;*/
                    }
                }
            }
        }             
    }
}
