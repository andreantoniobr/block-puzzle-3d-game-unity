using System;
using UnityEngine;

[Serializable]
public class GridCell
{
    [SerializeField] private Vector2Int position;
    [SerializeField] private bool isFull;

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
