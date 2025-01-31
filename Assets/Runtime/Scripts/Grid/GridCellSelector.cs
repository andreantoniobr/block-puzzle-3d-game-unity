using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellSelector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    public static event Action<GridCell> GridCellHoverEvent;
    public static event Action<GridCell> GridCellSelectedEvent;

    private void Update()
    {
        UpdateGridCell();
    }

    private void UpdateGridCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputController.InputPosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            GetGridCell(raycastHit);
        }
    }

    private void GetGridCell(RaycastHit raycastHit)
    {
        GridBackgroundCell gridBackgroundCell = raycastHit.transform.GetComponent<GridBackgroundCell>();
        if (gridBackgroundCell)
        {
            GridCell gridCell = gridBackgroundCell.GridCell;
            if (gridCell)
            {
                GridCellHoverEvent?.Invoke(gridCell);
                if (!InputController.IsHolding)
                {
                    GridCellSelectedEvent?.Invoke(gridCell);
                }
            }
        }        
    }
}
