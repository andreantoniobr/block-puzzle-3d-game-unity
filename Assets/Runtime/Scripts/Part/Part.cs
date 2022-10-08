using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Part : MonoBehaviour
{
    [SerializeField] private GridPartData gridPartData;

    public GridPartData GridPartData
    {
        get => gridPartData;
        set => gridPartData = value;
    }
}
