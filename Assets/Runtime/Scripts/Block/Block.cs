using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] private BlockData blockData;
    [SerializeField] private GridCellModel gridCellModel;
    [SerializeField] private BlockColor blockColor;

    private List<GridCellModel> gridCellModels = new List<GridCellModel>();

    public BlockData BlockData
    {
        get => blockData;
        set => blockData = value;
    }

    public BlockColor BlockColor
    {
        get => blockColor;
        set { 
            blockColor = value;
            SetBlockMaterialByColor();
        }
    }

    public void GenerateBlockModel(float blockSize, float blockPositionZ)
    {
        if (blockData && blockData.BlockCells.Count > 0)
        {
            int blockRowsAmount = BlockDataConstants.RowsAmount;
            int blockCollsAmount = BlockDataConstants.CollsAmount;
            Vector2Int gridSize = new Vector2Int(blockRowsAmount, blockCollsAmount);
            for (int x = 0; x < blockRowsAmount; x++)
            {
                for (int y = 0; y < blockCollsAmount; y++)
                {
                    if (BlockDataHelper.IsFullBlockCell(x, y, blockData))
                    {
                        InstantiateGridCellModel(x, y, gridSize, blockSize, blockPositionZ);
                    }                    
                }
            }
        }
    }

    private void InstantiateGridCellModel(int x, int y, Vector2Int gridSize, float blockSize, float blockPositionZ)
    {
        if (gridCellModel)
        {
            Vector3 gridCellModelPosition = MathHelper.GetGridCellPosition(x, y, gridSize, blockSize, blockPositionZ);
            GridCellModel currentGridCellModel = Instantiate(gridCellModel, gridCellModelPosition, Quaternion.identity, transform);
            if (currentGridCellModel)
            {
                gridCellModels.Add(currentGridCellModel);
                currentGridCellModel.name = $"blockModel[{x},{y}]";                
            }
        }        
    }

    private void SetBlockMaterialByColor() 
    {
        int cridCellsModelsAmount = gridCellModels.Count;
        if (cridCellsModelsAmount > 0)
        {
            for (int i = 0; i < cridCellsModelsAmount; i++)
            {
                GridCellModel currentGridCellModel = gridCellModels[i];
                if (currentGridCellModel)
                {
                    currentGridCellModel.SetMaterial(blockColor);
                }
            }
        }            
    }
}
