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
            gridCell.GridCellColorChangeEvent += OnGridCellColorChangeEvent;
        }
    }

    private void UnsubscribeInEvents()
    {
        if (gridCell)
        {
            gridCell.GridCellChangeEvent -= OnGridCellChangeState;
            gridCell.GridCellHoverEvent -= OnHoverGridCell;
            gridCell.GridCellColorChangeEvent -= OnGridCellColorChangeEvent;
        }
    }

    private void OnGridCellChangeState(bool isActive)
    {
        SetGridCellModelActive(isActive);
    }

    private void OnHoverGridCell(bool isActive)
    {        
        SetGridCellModelhover(isActive);
    }

    private void OnGridCellColorChangeEvent(BlockColor blockColor)
    {
        if (gridCellModell)
        {
            gridCellModell.SetMaterial(blockColor);
        }
    }

    private void SetGridCellModelActive(bool isActive)
    {
        if (gridCellModell)
        {
            gridCellModell.gameObject.SetActive(isActive);
        }
    }

    private void SetGridCellModelhover(bool isActive)
    {
        if (gridCellModelHover)
        {
            gridCellModelHover.gameObject.SetActive(isActive);
        }
    }
}
