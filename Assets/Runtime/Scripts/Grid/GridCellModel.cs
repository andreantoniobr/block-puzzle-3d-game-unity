using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct GridCellModelMaterial
{
    public Material Material;
    public BlockColor BlockColor;
}

public class GridCellModel : MonoBehaviour
{
    [SerializeField] private GridCellModelMaterial[] gridCellModelMaterials;

    MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }     

    public void SetMaterial(BlockColor blockColor)
    {
        if (meshRenderer)
        {
            Material material = GetMaterialByColor(blockColor);
            if (material)
            {
                meshRenderer.material = material;
            }            
        }
    }

    private Material GetMaterialByColor(BlockColor blockColor)
    {
        Material material = null;
        int materialsAmount = gridCellModelMaterials.Length;
        if (materialsAmount > 0)
        {
            for (int i = 0; i < materialsAmount; i++)
            {
                GridCellModelMaterial gridCellModelMaterial = gridCellModelMaterials[i];                
                if (MathHelper.IsInArrayRange(i, materialsAmount) && gridCellModelMaterial.BlockColor == blockColor)
                {
                    Material currentMaterial = gridCellModelMaterial.Material;
                    if (currentMaterial)
                    {
                        material = currentMaterial;
                        break;
                    }
                }
            }
        }
        return material;
    }
}
