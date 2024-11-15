using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MuHua;

/// <summary>
/// 核心模块，实现业务逻辑
/// </summary>
public class ModuleCore : Module<ModuleCore> {

    #region 资产模块
    /// <summary> 板片资产 </summary>
    public ModuleAssets<DataPlate> AssetsPlate;
    /// <summary> 预设板片资产 </summary>
    public ModuleAssets<DataPlatePresets> AssetsPlatePresets;
    #endregion

    #region 页面模块
    /// <summary> 不会被销毁的全局唯一页面模块 (UIDocument) </summary>
    public ModuleUIPage GlobalPage;
    /// <summary> 当前的主要页面模块 (UIDocument) </summary>
    public ModuleUIPage CurrentPage;
    /// <summary> 预设模板窗口 (回调Action) </summary>
    public ModuleUIWindow<Action> PresetsPlateWindow;
    #endregion

    #region 视图模块
    /// <summary> 设计视图相机模块 </summary>
    public ModuleViewCamera ViewCameraDesign;
    /// <summary> 板片烘焙相机视图 </summary>
    public ModuleViewCamera ViewCameraBaking;
    #endregion

    #region 广播模块
    /// <summary> 广播板片数据模块 </summary>
    public ModuleSending<DataPlate> SendingPlate;
    /// <summary> 广播板片数据点模块 </summary>
    public ModuleSending<DataPoint> SendingPoint;
    /// <summary> 广播查询数据模块 </summary>
    public ModuleSending<DataFindPoint> SendingFindPoint;
    #endregion

    #region 输入模块
    /// <summary> 设计UI输入模块 </summary>
    public ModuleUIInput<UIInputDesignUnit> UIInputDesign;
    #endregion

    #region 转换模块
    /// <summary> 预设板片转换板片 </summary>
    public ModuleBuilder<DataPlatePresets, DataPlate> PlatePresetsToPlate;
    /// <summary> 插入点数据转换板片上的点 </summary>
    public ModuleBuilder<DataInsertPoint, DataPoint> InsertPointToPoint;
    #endregion

    #region 可视模块
    /// <summary> 板片可视化内容生成模块 </summary>
    public ModuleVisual<DataPlate> VisualPlate;
    /// <summary> 点可视化内容生成模块 </summary>
    public ModuleVisual<DataPoint> VisualPoint;
    /// <summary> 多边形可视化内容生成模块 </summary>
    public ModuleVisual<DataPolygon> VisualPolygon;
    #endregion

    #region 算法模块

    /// <summary> 查询点算法模块 </summary>
    public ModuleAlgorithm<DataFindPoint> AlgorithmFindPoint;
    /// <summary> 查询贝塞尔点算法模块 </summary>
    public ModuleAlgorithm<DataFindBezier> AlgorithmFindBezier;

    /// <summary> 插入点算法模块 </summary>
    public ModuleAlgorithm<DataInsertPoint> AlgorithmInsertPoint;

    /// <summary> 多边形算法模块 </summary>
    public ModuleAlgorithm<DataPlate> AlgorithmPolygon;
    ///// <summary> 边缘排序算法模块 </summary>
    //public ModuleAlgorithm<DataPlate> EdgeSort = new AlgorithmEdge();

    #endregion
}
