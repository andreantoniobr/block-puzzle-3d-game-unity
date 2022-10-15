using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BlockPosition
{
    public Vector3 Position;
    public bool IsFull;
}

public class BlockPositionController : MonoBehaviour
{
    [SerializeField] private BlockPosition[] blocksPositions;

    private void Awake()
    {
        BlocksController.AvaliableBlocksEvent += OnAvaliableBlocks;
    }

    private void OnDestroy()
    {
        BlocksController.AvaliableBlocksEvent -= OnAvaliableBlocks;
    }

    private void OnAvaliableBlocks(List<Block> avaliableBlock)
    {
        SetAllVaganciesPositions();
        SetBlocksPositions(avaliableBlock);
    }

    private void SetAllVaganciesPositions()
    {
        int blocksPositionsAmount = blocksPositions.Length;
        if (blocksPositionsAmount > 0)
        {
            for (int i = 0; i < blocksPositionsAmount; i++)
            {                
                if (MathHelper.IsInArrayRange(i, blocksPositionsAmount))
                {
                    blocksPositions[i].IsFull = false;
                }                
            }            
        }
    }
    

    private void SetBlocksPositions(List<Block> avaliableBlocks)
    {
        for (int i = 0; i < avaliableBlocks.Count; i++)
        {
            Block block = avaliableBlocks[i];
            if (block)
            {                
                SetBlockPosition(block);
            }
        }
    }

    private void SetBlockPosition(Block block)
    {
        if (block)
        {
            for (int i = 0; i < blocksPositions.Length; i++)
            {
                if (!blocksPositions[i].IsFull)
                {
                    block.transform.position = blocksPositions[i].Position;
                    blocksPositions[i].IsFull = true;
                    break;
                }
            }
        }        
    }
}
