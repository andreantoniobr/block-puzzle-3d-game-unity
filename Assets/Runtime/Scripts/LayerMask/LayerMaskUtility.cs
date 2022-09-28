using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerMaskUtility : MonoBehaviour
{
    public static int GetLayerMask(LayerMask layerMask)
    {
        return (int)Mathf.Log(layerMask.value, 2);
    }
}
