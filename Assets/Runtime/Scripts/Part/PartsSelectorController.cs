using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSelectorController : MonoBehaviour
{
    [SerializeField] private LayerMask layerMask;
    public static event Action<Part> PartSelectedEvent;
    
    private void Update()
    {
        if (InputController.IsHolding)
        {
            TryGetPart();
        }
    }

    private void TryGetPart()
    {
        if (RaycastHelper.GetRaycast(InputController.InputPosition, layerMask, out RaycastHit raycastHit))
        {
            Part part = raycastHit.transform.GetComponent<Part>();
            if (part)
            {
                PartSelectedEvent?.Invoke(part);
            }
        }
    }
}
