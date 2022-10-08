using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsController : MonoBehaviour
{
    [SerializeField] private GridPartData[] partsData;

    public static event Action<GridPartData> PartChangeEvent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            GridPartData gridPartData = GetRandomPart();
            if (gridPartData)
            {
                PartChangeEvent?.Invoke(gridPartData);
            }            
        }
    }

    private GridPartData GetRandomPart()
    {
        GridPartData gridPartData = null;
        int partsDataAmount = partsData.Length;
        if (partsDataAmount > 0)
        {
            int randomPartIndex = UnityEngine.Random.Range(0, partsDataAmount);
            if (randomPartIndex >= 0 && randomPartIndex < partsDataAmount)
            {
                gridPartData = partsData[randomPartIndex];
            }
        }
        return gridPartData;
    }
}
