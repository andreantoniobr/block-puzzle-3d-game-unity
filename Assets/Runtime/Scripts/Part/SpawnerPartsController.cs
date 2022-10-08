using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerPartsController : MonoBehaviour
{
    [SerializeField] private Part partModel;
    [SerializeField] private GridPartData[] gridPartDatas;

    public static event Action<Part> SpawnPartEvent;

    private void Start()
    {
        SpawnAllParts();
    }

    private void SpawnAllParts()
    {
        if (gridPartDatas.Length > 0)
        {
            for (int i = 0; i < gridPartDatas.Length; i++)
            {
                Part part = GetPart(gridPartDatas[i]);
                if (part)
                {
                    SpawnPartEvent?.Invoke(part);                    
                }
            }
        }
    }    

    private Part GetPart(GridPartData gridPartData)
    {
        Part part = null;
        if (gridPartData)
        {
            part = Instantiate(partModel, transform);
            if (part)
            {
                part.GridPartData = gridPartData;
            }
        }
        return part;
    }
}
