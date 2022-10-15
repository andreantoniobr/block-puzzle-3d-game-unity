using UnityEngine;

public class MathHelper : MonoBehaviour
{
    public static int GetGridsAmountInMainGrid(int gridX, int gridY, int mainGridX, int mainGridY)
    {
        return GetGridsAmountInSideOfMainGrid(gridX, mainGridX) * GetGridsAmountInSideOfMainGrid(gridY, mainGridY);
    }

    public static int GetGridsAmountInSideOfMainGrid(int gridSize, int mainGridSize)
    {
        int gridsAmount = 0;
        if (IsIntDivisible(mainGridSize, gridSize))
        {
            gridsAmount = mainGridSize / gridSize;
        }
        return gridsAmount;
    }

    public static Vector3 GetGridCellPosition(int gridCellX, int gridCellY, Vector2Int gridSize, float gridCellSize, float gridCellPositionZ)
    {
        float gridCellPositionDecrementX = GetGridCellPositionDecrement(gridSize.x);
        float gridCellPositionDecrementY = GetGridCellPositionDecrement(gridSize.y);
        float positionX = (gridCellX - gridCellPositionDecrementX) * gridCellSize;
        float positionY = (gridCellY - gridCellPositionDecrementY) * gridCellSize;
        return new Vector3(positionX, positionY, gridCellPositionZ);
    }

    public static float GetGridCellPositionDecrement(int gridSize)
    {
        float b = (gridSize / 2) - 0.5f;
        if (gridSize % 2 != 0)
        {
            b = (gridSize - 1) / 2;
        }
        return b;
    }

    public static bool IsIntDivisible(int a, int b)
    {
        return b > 0 && a % b == 0;
    }

    public static bool IsInArrayRange(int index, int maxIndexInRange)
    {
        return index >= 0 && index < maxIndexInRange;
    }

    public static int GetIntLayerMaskValue(LayerMask layerMask)
    {
        return (int) Mathf.Log(layerMask.value, 2);
    }
}
