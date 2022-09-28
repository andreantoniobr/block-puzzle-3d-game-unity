using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPartsDataConstants
{
    public const int CollsAmount = 3;
    public const int RowsAmount = 3;
}

[CreateAssetMenu(fileName = "GridPartData", menuName = "Parts/New Part", order = 1)]
public class GridPartsData : ScriptableObject
{
    [SerializeField] private bool[] gridParts = new bool[GridPartsDataConstants.RowsAmount * GridPartsDataConstants.CollsAmount];

    public bool[] GridParts 
    {
        get => gridParts;
        set => gridParts = value;
    }
}