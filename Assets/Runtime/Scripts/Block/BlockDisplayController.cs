using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockDisplayController : MonoBehaviour
{
    private void Awake()
    {
        BlockSpawnController.SpawnBlockEvent += OnSpawnBlock;
        BlocksController.AvaliableBlocksEvent += OnAvaliableBlocks;
    }

    private void OnDestroy()
    {
        BlockSpawnController.SpawnBlockEvent -= OnSpawnBlock;
        BlocksController.AvaliableBlocksEvent -= OnAvaliableBlocks;
    }

    private void OnSpawnBlock(Block block)
    {
        HideBlock(block);
    }

    private void OnAvaliableBlocks(List<Block> avaliableBlocks)
    {
        if (avaliableBlocks.Count > 0)
        {
            for (int i = 0; i < avaliableBlocks.Count; i++)
            {
                ShowBlock(avaliableBlocks[i]);
            }
        }        
    }

    private void HideBlock(Block block)
    {
        SetActiveBlock(block, false);
    }

    private void ShowBlock(Block block)
    {
        SetActiveBlock(block, true);
    }

    private void SetActiveBlock(Block block, bool isActive)
    {
        if (block)
        {
            block.gameObject.SetActive(isActive);
        }        
    }
}
