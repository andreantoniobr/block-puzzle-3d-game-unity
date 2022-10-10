using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataConstants
{
    public const int CollsAmount = 3;
    public const int RowsAmount = 3;
}

[CreateAssetMenu(fileName = "BlockData", menuName = "Block/New", order = 1)]
public class BlockData : ScriptableObject
{
    [SerializeField] private List<bool> blockCells = new List<bool>(new bool[BlockDataConstants.RowsAmount * BlockDataConstants.CollsAmount]);
    //[SerializeField] private bool[] gridParts = new bool[GridPartsDataConstants.RowsAmount * GridPartsDataConstants.CollsAmount];

    public List<bool> BlockCells 
    {
        get => blockCells;
        set => blockCells = value;
    }
}