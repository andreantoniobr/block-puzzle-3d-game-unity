using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridPartDataConstants
{
    public const int CollsAmount = 3;
    public const int RowsAmount = 3;
}

[CreateAssetMenu(fileName = "GridPartData", menuName = "Parts/New Part", order = 1)]
public class GridPartData : ScriptableObject
{
    [SerializeField] private List<bool> gridPart = new List<bool>(new bool[GridPartDataConstants.RowsAmount * GridPartDataConstants.CollsAmount]);
    //[SerializeField] private bool[] gridParts = new bool[GridPartsDataConstants.RowsAmount * GridPartsDataConstants.CollsAmount];

    public List<bool> GridPart 
    {
        get => gridPart;
        set => gridPart = value;
    }
}