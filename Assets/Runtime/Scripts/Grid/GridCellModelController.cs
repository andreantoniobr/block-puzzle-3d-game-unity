using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridCell))]
public class GridCellModelController : MonoBehaviour
{
    [SerializeField] private GridCellModel gridCellModell;
    [SerializeField] private GridCellModel gridCellModelHover;

    private GridCell gridCell;

    private void Awake()
    {
        gridCell = GetComponent<GridCell>();
        SubscribeInEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeInEvents();
    }

    private void SubscribeInEvents()
    {
        if (gridCell)
        {
            gridCell.GridCellChangeEvent += OnGridCellChangeState;
            gridCell.GridCellHoverEvent += OnHoverGridCell;
        }
    }

    private void UnsubscribeInEvents()
    {
        if (gridCell)
        {
            gridCell.GridCellChangeEvent -= OnGridCellChangeState;
            gridCell.GridCellHoverEvent += OnHoverGridCell;
        }
    }

    private void OnGridCellChangeState(bool isActive)
    {
        if (gridCellModell)
        {
            gridCellModell.gameObject.SetActive(isActive);
        }        
    }

    private void OnHoverGridCell(bool isActive)
    {
        gridCellModelHover.gameObject.SetActive(isActive);
    }
}
