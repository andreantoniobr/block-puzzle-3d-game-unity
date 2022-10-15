using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockData blockData;
    [SerializeField] private BlockModel blockModel;

    public BlockData BlockData
    {
        get => blockData;
        set => blockData = value;
    }

    public void GenerateBlockModel(float blockSize, float blockPositionZ)
    {
        if (blockData && blockData.BlockCells.Count > 0)
        {
            int blockRowsAmount = BlockDataConstants.RowsAmount;
            int blockCollsAmount = BlockDataConstants.CollsAmount;
            Vector2Int gridSize = new Vector2Int(blockRowsAmount, blockCollsAmount);
            for (int x = 0; x < blockRowsAmount; x++)
            {
                for (int y = 0; y < blockCollsAmount; y++)
                {
                    if (BlockDataHelper.IsFullBlockCell(x, y, blockData))
                    {
                        InstantiateBlockModel(x, y, gridSize, blockSize, blockPositionZ);
                    }                    
                }
            }
        }
    }

    private void InstantiateBlockModel(int x, int y, Vector2Int gridSize, float blockSize, float blockPositionZ)
    {
        if (blockModel)
        {
            Vector3 blockModelPosition = MathHelper.GetGridCellPosition(x, y, gridSize, blockSize, blockPositionZ);
            BlockModel currentblockModel = Instantiate(blockModel, blockModelPosition, Quaternion.identity, transform);
            if (currentblockModel)
            {
                currentblockModel.name = $"blockModel[{x},{y}]";
            }
        }        
    }
}
