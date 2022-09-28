using UnityEngine;
using System;

[Serializable]
public struct GridCellData
{
    [SerializeField] private GridCell gridCell;
    [SerializeField] private Vector2Int position;
    [SerializeField] private bool isFull;

    public GridCell GridCell
    {
        get => gridCell;
        set => gridCell = value;
    }

    public Vector2Int Position
    {
        get => position;
        set => position = value;
    }

    public bool IsFull
    {
        get => isFull;
        set => isFull = value;
    }
}
