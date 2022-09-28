using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridGenerator))]
public class GridCellController : MonoBehaviour
{    
    [SerializeField] private GridCellModel selectedGridCellModel;
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

    private void OnGridCellSelected(GridCellModel gridCellModel)
    {
        if (gridGenerator && gridCellModel)
        {
            selectedGridCellModel = gridCellModel;
            gridCellModel.SetState(false);
        }
    }

    /*
    private void SetGridPartInGridCells(GridCell gridCell, GridPartsData gridPartsData)
    {
        if (gridCell && gridPartsData)
        {
            for (int x = 0; x < GridPartsDataConstants.RowsAmount; x++)
            {
                for (int y = 0; y < GridPartsDataConstants.CollsAmount; y++)
                {
                    if (gridPartsData.GridParts[x * GridPartsDataConstants.RowsAmount + y])
                    {
                        Vector2Int gridCellDataPosition = gridCell.GridCellData.Position;
                        gridCell.SetGridCellDataState(true);
                        gridGenerator.GridCellData[gridCellDataPosition.x, gridCellDataPosition.y].IsFull = true;
                    }
                }
            }
        }             
    }*/
}
