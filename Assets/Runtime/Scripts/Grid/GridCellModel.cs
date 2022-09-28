using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCellModel : MonoBehaviour
{
    [SerializeField] private BlockModel blockModell;
    [SerializeField] private GridCell gridCell;

    public GridCell GridCell
    {
        get => gridCell;
        set => gridCell = value;
    }

    public void SetState(bool isFull)
    {
        gridCell.IsFull = isFull;
        ChangeGridCellModelState(isFull);
    }

    private void ChangeGridCellModelState(bool isActive)
    {
        if (blockModell)
        {
            blockModell.gameObject.SetActive(isActive);
        }        
    }
}
