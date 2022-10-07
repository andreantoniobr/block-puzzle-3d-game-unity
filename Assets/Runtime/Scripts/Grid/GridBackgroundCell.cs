using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridBackgroundCell : MonoBehaviour
{
    [SerializeField] private GridCell gridCell;

    public GridCell GridCell 
    {
        get => gridCell;
        set => gridCell = value;
    }
}
