using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 核心模块，实现业务逻辑
/// </summary>
public class ModuleCore : Module<ModuleCore> {

    #region 页面模块
    /// <summary> 不会被销毁的全局唯一页面模块 (UIDocument) </summary>
    public ModuleUIPage GlobalPage;
    /// <summary> 当前的主要页面模块 (UIDocument) </summary>
    public ModuleUIPage CurrentPage;
    #endregion

}
