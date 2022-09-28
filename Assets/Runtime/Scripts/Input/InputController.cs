using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    public static Vector3 GetInput()
    {
        return Input.mousePosition;
    }
}
