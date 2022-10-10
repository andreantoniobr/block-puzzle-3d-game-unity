using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSelectionController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public static event Action<Block> BlockSelectedEvent;
    
    private void Update()
    {
        if (InputController.IsHolding)
        {
            TryGetBlock();
        }
    }

    private void TryGetBlock()
    {
        if (RaycastHelper.GetRaycast(InputController.InputPosition, layerMask, out RaycastHit raycastHit))
        {
            Block block = raycastHit.transform.GetComponent<Block>();
            if (block)
            {
                BlockSelectedEvent?.Invoke(block);
            }
        }
    }
}
