using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDataHelper : MonoBehaviour
{
    public static bool IsFullBlockCell(int x, int y, BlockData blockData)
    {
        bool isFullBlockCell = false;
        if (ExistBlockData(blockData))
        {
            int i = x + y * BlockDataConstants.RowsAmount;
            int maxIndexInRange = blockData.BlockCells.Count;
            if (MathHelper.IsInArrayRange(i, maxIndexInRange))
            {
                isFullBlockCell = blockData.BlockCells[i];
            }
        }        
        return isFullBlockCell;
    }

    public static bool ExistBlockData(BlockData blockData)
    {
        return blockData && blockData.BlockCells.Count > 0;
    }
}
