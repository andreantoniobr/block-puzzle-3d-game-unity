using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawnController : MonoBehaviour
{
    [SerializeField] private Block blockModel;
    [SerializeField] private BlockData[] blockDatas;
    [SerializeField] List<Block> spawnedBlocks;

    public List<Block> SpawnedBlocks => spawnedBlocks;
    public static event Action<Block> SpawnBlockEvent;

    private void Start()
    {
        SpawnAllBlocks();
    }

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
                    SpawnBlockEvent?.Invoke(block);                    
                }
            }
        }
    }    

    private Block GetBlock(BlockData blockData)
    {
        Block block = null;
        if (blockData)
        {
            block = Instantiate(blockModel, transform);
            if (block)
            {
                block.BlockData = blockData;
            }
        }
        return block;
    }
}
