using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public static class ModuleCoreTool {
    public static void FunctionRegister<Module>(this ModuleCore core, Module module) {
        Type baseType = module.GetType().BaseType;
        FieldInfo fieldInfo = FindField<ModuleCore>(baseType);
        if (fieldInfo == null) {
            Debug.LogError($"{typeof(ModuleCore).Name} 类型没有 {baseType.Name} 字段!");
            return;
        }
        object obj = fieldInfo.GetValue(core);
        if (obj != null) {
            Debug.LogWarning($"{module.GetType().Name} 模块替换了 {obj.GetType().Name} 模块!");
            return;
        }
        fieldInfo.SetValue(core, module);
    }

    public static FieldInfo FindField<T>(Type baseType) {
        FieldInfo[] fileInfos = typeof(T).GetFields();
        for (int i = 0; i < fileInfos.Length; i++) {
            if (fileInfos[i].FieldType == baseType) { return fileInfos[i]; }
        }
        return null;
    }
}
