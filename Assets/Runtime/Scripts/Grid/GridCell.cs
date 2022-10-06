using System;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    [SerializeField] private Vector2Int position;
    [SerializeField] private GridCellColor gridCellColor;
    [SerializeField] private bool isFull;
    [SerializeField] private bool isHover;

    public event Action<bool> GridCellChangeEvent;
    public event Action<bool> GridCellHoverEvent;

    public Vector2Int Position
    {
        get => position;
        set => position = value;
    }

    public GridCellColor GridCellColor
    {
        get => gridCellColor;
        set => gridCellColor = value;
    }

    public bool IsFull
    {
        get => isFull;
        set
        {
            isFull = value;
            GridCellChangeEvent?.Invoke(value);
        }
    }

    public bool IsHover
    {
        get => isHover;
        set
        {
            isHover = value;
            GridCellHoverEvent?.Invoke(value);
        }
    }

    private void Start()
    {
        GridCellChangeEvent?.Invoke(isFull);
        GridCellHoverEvent?.Invoke(isHover);
    }
}
