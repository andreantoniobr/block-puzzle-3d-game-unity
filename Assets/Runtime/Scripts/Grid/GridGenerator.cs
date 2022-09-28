using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(GridDataGenerator))]
[RequireComponent(typeof(GridDisplay))]
public class GridGenerator : MonoBehaviour
{
    private GridDataGenerator gridDataGenerator;
    private GridDisplay gridDisplay;
    private GridCell[,] gridCellData;

    public GridCell[,] GridCellData => gridCellData;

    private void Awake()
    {
        gridDataGenerator = GetComponent<GridDataGenerator>();
        gridDisplay = GetComponent<GridDisplay>();
    }

    private void Start()
    {
        GenerateGrid();
    }

    private void GenerateGrid()
    {
        if (gridDataGenerator && gridDisplay)
        {
            gridCellData = gridDataGenerator.GetGridCellData();
            gridDisplay.Display(gridCellData);
        }
    }
}
