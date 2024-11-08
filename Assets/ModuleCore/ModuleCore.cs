using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

public class ModuleCore : Module<ModuleCore> {

    #region 资产模块
    /// <summary> 预设模板资产 </summary>
    public ModuleAssets<DataPresetsPlate> PresetsPlateAssets;
    #endregion

    #region 页面模块
    /// <summary> 不会被销毁的全局唯一页面模块 (UIDocument) </summary>
    public ModuleUIPage GlobalPage;
    /// <summary> 当前的主要页面模块 (UIDocument) </summary>
    public ModuleUIPage CurrentPage;
    /// <summary> 预设模板窗口 (回调Action) </summary>
    public ModuleUIWindow<Action> PresetsPlateWindow;
    #endregion

    #region 功能模块
    /// <summary> 代理模块 </summary>
    public ModuleAgent ModuleAgent;
    /// <summary> 根据设计点生成边缘算法模块 </summary>
    public ModuleAlgorithm<DataPlate> GenerateEdge = new AlgorithmGenerateEdge();
    /// <summary> 边缘排序算法模块 </summary>
    public ModuleAlgorithm<DataPlate> EdgeSort = new AlgorithmEdge();
    /// <summary> 多边形算法模块 </summary>
    public ModuleAlgorithm<DataPlate> Polygon = new AlgorithmPolygon();
    /// <summary> 板片设计模块 </summary>
    public ModulePlateDesign PlateDesign;
    /// <summary> 板片设计相机视图 </summary>
    public ModuleViewCamera PlateDesignViewCamera;
    /// <summary> 板片烘焙相机视图 </summary>
    public ModuleViewCamera PlateBakingViewCamera;
    #endregion

    #region 控制模块
    /// <summary> 板片设计输入模块 </summary>
    public ModuleViewInput PlateDesignViewInput;
    /// <summary> 板片烘焙输入模块 </summary>
    public ModuleViewInput PlateBakingViewInput;
    #endregion
}
