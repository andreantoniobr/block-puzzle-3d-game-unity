using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellSelector : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;

    public static event Action<GridCellModel> GridCellSelectedEvent;

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(InputController.GetInput());
        if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, layerMask))
        {
            GetGridCell(raycastHit);
        }
    }

    private void GetGridCell(RaycastHit raycastHit)
    {
        GridCellModel gridCellModel = raycastHit.transform.GetComponent<GridCellModel>();
        if (gridCellModel)
        {
            GridCellSelectedEvent?.Invoke(gridCellModel);
        }        
    }
}
