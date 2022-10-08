using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static bool IsHolding => GetIsHolding();
    public static Vector3 InputPosition => GetInputPosition();

    private static Vector3 GetInputPosition()
    {
        return Input.mousePosition;
    }
    
    private static bool GetIsHolding()
    {
        return Input.GetMouseButton(0);
    }
}
