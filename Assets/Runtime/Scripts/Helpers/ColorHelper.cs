using System;
using UnityEngine;

public class ColorHelper : MonoBehaviour
{
    public static BlockColor RandomColor => GetColor();

    private static BlockColor GetColor()
    {
        BlockColor color;
        while (true)
        {
            color = GetRandomColor();
            if (color != BlockColor.Default)
            {
                break;
            }
        }
        return color;
    }

    private static BlockColor GetRandomColor()
    {
        Array values = Enum.GetValues(typeof(BlockColor));
        int randomBlockColorIndex = UnityEngine.Random.Range(0, values.Length);
        return (BlockColor) values.GetValue(randomBlockColorIndex);
    }
}
