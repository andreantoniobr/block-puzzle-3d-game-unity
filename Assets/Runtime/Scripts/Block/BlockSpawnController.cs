using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnController : MonoBehaviour
{
    [SerializeField] private Block block;
    [SerializeField] private BlockData[] blockDatas;
    [SerializeField] List<Block> spawnedBlocks;
    
    [SerializeField] private float blockSize;
    [SerializeField] private float blockPositionZ;

    public List<Block> SpawnedBlocks => spawnedBlocks;
    public static event Action<Block> SpawnBlockEvent;

    public void SpawnAllBlocks()
    {
        if (blockDatas.Length > 0)
        {
            for (int i = 0; i < blockDatas.Length; i++)
            {
                Block block = GetBlock(blockDatas[i]);
                if (block)
                {
                    spawnedBlocks.Add(block);
                    block.GenerateBlockModel(blockSize, blockPositionZ);
                    SpawnBlockEvent?.Invoke(block);                    
                }
            }
        }
    }

    private Block GetBlock(BlockData blockData)
    {
        Block currentBlock = null;
        if (blockData && block)
        {
            currentBlock = Instantiate(block, transform);
            if (currentBlock)
            {
                currentBlock.BlockData = blockData;
            }
        }
        return currentBlock;
    }
}
