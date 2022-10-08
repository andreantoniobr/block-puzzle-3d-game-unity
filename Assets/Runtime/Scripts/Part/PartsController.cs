using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsController : MonoBehaviour
{
    [SerializeField] private List<Part> allParts;
    [SerializeField] private List<Part> avaliableParts;
    [SerializeField] private Part selectedMainPart;

    private readonly int partsAmount = 3;    

    public static event Action<List<Part>> AvaliablePartsEvent;
    public static event Action<Part> SelectedMainPartEvent;

    public List<Part> AllParts => allParts;

    private void Awake()
    {
        SpawnerPartsController.SpawnPartEvent += OnSpawnPart;
        PartsSelectorController.PartSelectedEvent += OnPartSelectedEvent;
    }

    private void OnDestroy()
    {
        SpawnerPartsController.SpawnPartEvent -= OnSpawnPart;
        PartsSelectorController.PartSelectedEvent -= OnPartSelectedEvent;
    }

    private void Start()
    {
        GetAvaliableParts();        
    }

    private void OnSpawnPart(Part newPart)
    {
        AddPart(newPart);
    }

    private void OnPartSelectedEvent(Part part)
    {
        if (selectedMainPart != part)
        {
            selectedMainPart = part;
        }
    }

    private void AddPart(Part newPart)
    {
        if (NotContains(newPart, allParts))
        {
            allParts.Add(newPart);
        }
    }

    private void GetAvaliableParts()
    {
        int selectedPartsAmount = 0;
        while (selectedPartsAmount < partsAmount)
        {
            Part part = GetRandomPart();
            if (part && NotContains(part, avaliableParts))
            {
                avaliableParts.Add(part);
                selectedPartsAmount++;
            }
        }
        if (avaliableParts.Count >= partsAmount)
        {            
            AvaliablePartsEvent?.Invoke(avaliableParts);
        }
    }

    private Part GetRandomPart()
    {
        Part part = null;
        int partsAmount = allParts.Count;
        if (partsAmount > 0)
        {
            int randomPartIndex = UnityEngine.Random.Range(0, partsAmount);
            if (randomPartIndex >= 0 && randomPartIndex < partsAmount)
            {
                part = allParts[randomPartIndex];
            }
        }
        return part;
    }

    private bool NotContains(Part currentPart, List<Part> parts)
    {
        return !parts.Contains(currentPart);
    }
}
