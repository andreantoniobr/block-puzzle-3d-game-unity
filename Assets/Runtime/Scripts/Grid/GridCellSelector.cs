using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellSelector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    public static event Action<GridCell> GridCellSelectedEvent;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(InputController.GetInput());
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
            {
                GetGridCell(raycastHit);
            }
        }        
    }

    private void GetGridCell(RaycastHit raycastHit)
    {
        GridCell gridCell = raycastHit.transform.GetComponent<GridCell>();
        if (gridCell)
        {
            GridCellSelectedEvent?.Invoke(gridCell);
        }        
    }
}
