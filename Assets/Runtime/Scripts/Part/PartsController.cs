using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpawnerPartsController))]
public class PartsController : MonoBehaviour
{
    [SerializeField] private List<Part> avaliableParts;
    [SerializeField] private Part selectedMainPart;

    private readonly int avaliablePartsAmount = 3;
    private SpawnerPartsController spawnerPartsController;

    public static event Action<List<Part>> AvaliablePartsEvent;
    public static event Action<Part> SelectedMainPartEvent;

    private void Awake()
    {
        spawnerPartsController = GetComponent<SpawnerPartsController>();
        PartsSelectorController.PartSelectedEvent += OnPartSelectedEvent;
    }

    private void OnDestroy()
    {
        PartsSelectorController.PartSelectedEvent -= OnPartSelectedEvent;
    }

    private void Start()
    {
        if (spawnerPartsController)
        {
            spawnerPartsController.SpawnAllParts();
            GetAvaliableParts();                       
        }                
    }    
    private void OnPartSelectedEvent(Part part)
    {
        if (selectedMainPart != part)
        {
            selectedMainPart = part;
            SelectedMainPartEvent?.Invoke(part);
        }
    }

    private void GetAvaliableParts()
    {
        int selectedPartsAmount = 0;
        if (spawnerPartsController.SpawnedParts.Count > 0)
        {
            while (selectedPartsAmount < avaliablePartsAmount)
            {
                Part part = GetRandomPart();
                if (part)
                {
                    if (NotContains(part, avaliableParts))
                    {
                        avaliableParts.Add(part);
                        selectedPartsAmount++;
                    }
                }
                else
                {
                    break;
                }
            }
        }
        
        if (avaliableParts.Count >= avaliablePartsAmount)
        {            
            AvaliablePartsEvent?.Invoke(avaliableParts);
        }
    }

    private Part GetRandomPart()
    {
        Part part = null;
        if (spawnerPartsController)
        {
            int partsAmount = spawnerPartsController.SpawnedParts.Count;
            if (partsAmount > 0)
            {
                int randomPartIndex = UnityEngine.Random.Range(0, partsAmount);
                if (randomPartIndex >= 0 && randomPartIndex < partsAmount)
                {
                    part = spawnerPartsController.SpawnedParts[randomPartIndex];
                }
            }
        }        
        return part;
    }

    private bool NotContains(Part currentPart, List<Part> parts)
    {
        return !parts.Contains(currentPart);
    }
}
