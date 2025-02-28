using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if ENABLE_INPUT_SYSTEM && UNITY_INPUT_SYSTEM_PACKAGE
using UnityEngine.InputSystem;
#endif

namespace MuHua
{
    /// <summary>
    /// UI工具
    /// </summary>
    public static class UITool
    {
        /// <summary> 获取鼠标位置 </summary>
        public static Vector3 GetMousePosition()
        {
#if ENABLE_INPUT_SYSTEM && UNITY_INPUT_SYSTEM_PACKAGE
            return Mouse.current.position.ReadValue();
#else
            return Input.mousePosition;
#endif
        }
    }
}