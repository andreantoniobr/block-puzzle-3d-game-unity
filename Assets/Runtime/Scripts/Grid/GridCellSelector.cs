using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellSelector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    [SerializeField] private GridCell gridCell;

    public static event Action<GridCell> GridCellHoverEvent;
    public static event Action<GridCell> GridCellSelectedEvent;

    private void Update()
    {
        UpdateGridCell();
    }

    private void UpdateGridCell()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputController.GetInput());
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
                if (Input.GetMouseButtonDown(0))
                {
                    GridCellSelectedEvent?.Invoke(gridCell);
                }
            }
        }        
    }
}
