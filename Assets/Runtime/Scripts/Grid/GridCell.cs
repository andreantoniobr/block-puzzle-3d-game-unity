using System;
using UnityEngine;


public class GridCell : MonoBehaviour
{
    [SerializeField] private Vector2Int position;
    [SerializeField] private bool isFull;

    public event Action<bool> GridCellChangeEvent;

    public Vector2Int Position
    {
        get => position;
        set => position = value;
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

    private void Start()
    {
        GridCellChangeEvent?.Invoke(isFull);
    }
}
