using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockData blockData;

    public BlockData BlockData
    {
        get => blockData;
        set => blockData = value;
    }
}
