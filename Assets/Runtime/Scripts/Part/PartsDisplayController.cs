using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartsDisplayController : MonoBehaviour
{
    private void Awake()
    {
        SpawnerPartsController.SpawnPartEvent += OnSpawnPart;
        PartsController.AvaliablePartsEvent += OnAvaliableParts;
    }

    private void OnDestroy()
    {
        SpawnerPartsController.SpawnPartEvent -= OnSpawnPart;
        PartsController.AvaliablePartsEvent -= OnAvaliableParts;
    }

    private void OnSpawnPart(Part newPart)
    {
        HidePart(newPart);
    }

    private void OnAvaliableParts(List<Part> avaliableParts)
    {
        if (avaliableParts.Count > 0)
        {
            for (int i = 0; i < avaliableParts.Count; i++)
            {
                ShowPart(avaliableParts[i]);
            }
        }        
    }

    private void HidePart(Part part)
    {
        SetActivePart(part, false);
    }

    private void ShowPart(Part part)
    {
        SetActivePart(part, true);
    }

    private void SetActivePart(Part part, bool isActive)
    {
        if (part)
        {
            part.gameObject.SetActive(isActive);
        }        
    }
}
