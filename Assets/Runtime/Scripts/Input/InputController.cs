using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static bool IsHolding => GetIsHolding();
    public static Vector3 InputPosition => GetInputPosition();

    private static Vector3 GetInputPosition()
    {

#if UNITY_EDITOR
        return Input.mousePosition;
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        return Input.GetTouch(0).position;
#endif
    }

    private static bool GetIsHolding()
    {
        bool isHolding = false;
#if UNITY_EDITOR
                isHolding = Input.GetMouseButton(0);
#endif

#if UNITY_ANDROID && !UNITY_EDITOR
        isHolding = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began;
        #endif
        return isHolding;
    }
}
