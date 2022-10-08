using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycastHelper : MonoBehaviour
{
    public static bool GetRaycast(Vector3 position, LayerMask layerMask, out RaycastHit raycastHit)
    {
        Ray ray = Camera.main.ScreenPointToRay(position);
        return Physics.Raycast(ray, out raycastHit, float.MaxValue, layerMask);
    }
}
