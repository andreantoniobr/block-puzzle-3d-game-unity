using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsSelectorController : MonoBehaviour
{
    [SerializeField] private List<Part> allParts;
    [SerializeField] private List<Part> avaliableParts;

    private int partsAmount = 3;

    public static event Action<List<Part>> AvaliablePartsEvent;
    public static event Action<Part> SelectedMainPartEvent;

    private void Awake()
    {
        SpawnerPartsController.SpawnPartEvent += OnSpawnPart;
    }

    private void OnDestroy()
    {
        SpawnerPartsController.SpawnPartEvent -= OnSpawnPart;
    }

    private void Start()
    {
        GetAvaliableParts();        
    }

    private void OnSpawnPart(Part newPart)
    {
        AddPart(newPart);
        HidePart(newPart);
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
            for (int i = 0; i < avaliableParts.Count; i++)
            {
                ShowPart(avaliableParts[i]);
            }
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

    private void HidePart(Part part)
    {
        part.gameObject.SetActive(false);
    }

    private void ShowPart(Part part)
    {
        part.gameObject.SetActive(true);
    }
}
