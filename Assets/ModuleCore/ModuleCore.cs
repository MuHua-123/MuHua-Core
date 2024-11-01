using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 核心模块，提供全部的抽象接口
/// </summary>
public class ModuleCore : Module<ModuleCore> {
    /*---------------------------------------------功能模块--------------------------------------------------------*/
    /// <summary> 视频模块  </summary>
    public ModuleVideo ModuleVideo;
    /// <summary> 场景模块  </summary>
    public ModuleScene ModuleScene;
    /// <summary> 代理模块  </summary>
    public ModuleAgent ModuleAgent;
    /*---------------------------------------------页面模块--------------------------------------------------------*/
    /// <summary> 当前的主要页面模块 (UIDocument) </summary>
    public ModuleUIPage CurrentPage;
    /// <summary> 不会被销毁的全局唯一页面模块 (UIDocument) </summary>
    public ModuleUIPage GlobalPage;
    /*---------------------------------------------页面模块--------------------------------------------------------*/
    /// <summary> 加载页面模块 (回调Action) </summary>
    public ModuleUIPanel<Action> LoadingPanel;
    /// <summary> 弹出提示模块 </summary>
    public ModuleUIPanel<string> PopupPromptPanel;
    /// <summary> 弹出窗口模块 </summary>
    public ModuleUIPanel<DataPopup> PopupWindowPanel;

    /// <summary> 设备视频交互模块 </summary>
    public ModuleUIPanel<DataVideo> VideoPanel;
    /// <summary> 设备视频图文交互模块 </summary>
    public ModuleUIPanel<DataVideoImage> VideoImagePanel;
    /// <summary> 学习视频模块 (回调Action) </summary>
    public ModuleUIPanel<Action> LearningVideoPanel;
    /// <summary> 全屏播放视频模块 (回调Action) </summary>
    public ModuleUIPanel<Action> FullScreenVideoPanel;
}
