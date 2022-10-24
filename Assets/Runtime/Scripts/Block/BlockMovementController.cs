using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlocksController))]
public class BlockMovementController : MonoBehaviour
{
    [SerializeField] private Block selectedMainBlock;
    [SerializeField] private float positionZ;
    [SerializeField] private LayerMask groundMask;

    private void Awake()
    {
        BlockSelectionController.BlockSelectedEvent += OnBlockSelected;
    }

    private void OnDestroy()
    {
        BlockSelectionController.BlockSelectedEvent -= OnBlockSelected;
    }

    private void OnBlockSelected(Block block)
    {
        if (selectedMainBlock != block)
        {
            selectedMainBlock = block;
        }
    }

    private void FixedUpdate()
    {
        if (selectedMainBlock && InputController.IsHolding && CanGetMousePosition(InputController.InputPosition, out Vector3 rayPosition))
        {
            Vector3 position = rayPosition;
            position.z = positionZ;
            selectedMainBlock.transform.position = position;
        }        
    }

    private bool CanGetMousePosition(Vector3 screenPoint, out Vector3 rayPosition)
    {
        rayPosition = Vector3.zero;
        bool canGetMousePosition = false;
        if (Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(screenPoint);
            if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, groundMask))
            {
                canGetMousePosition = true;
                rayPosition = raycastHit.point;
            }
        }
        return canGetMousePosition;
    }
}
