using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct PartPosition
{
    public Vector3 Position;
    public bool IsFull;
}

public class PartsPositionController : MonoBehaviour
{
    [SerializeField] private PartPosition[] partsPositions;

    private void Awake()
    {
        PartsSelectorController.AvaliablePartsEvent += OnAvaliableParts;
    }

    private void OnDestroy()
    {
        PartsSelectorController.AvaliablePartsEvent -= OnAvaliableParts;
    }

    private void OnAvaliableParts(List<Part> avaliableParts)
    {
        SetAvaliablePartsPositions(avaliableParts);
    }

    private void SetAvaliablePartsPositions(List<Part> avaliableParts)
    {
        for (int i = 0; i < avaliableParts.Count; i++)
        {
            Part part = avaliableParts[i];
            if (part)
            {                
                SetPartPosition(part);
            }
        }
    }

    private void SetPartPosition(Part part)
    {
        if (part)
        {
            for (int i = 0; i < partsPositions.Length; i++)
            {
                if (!partsPositions[i].IsFull)
                {
                    part.transform.position = partsPositions[i].Position;
                    partsPositions[i].IsFull = true;
                    break;
                }
            }
        }        
    }
}
