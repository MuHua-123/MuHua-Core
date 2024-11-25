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
    /// <summary> 板片资源管理模块 </summary>
    public ModuleAssets<DataPlate> AssetsPlate;
    /// <summary> 预设板片资产 </summary>
    public ModuleAssets<DataPlatePresets> AssetsPlatePresets;
    /// <summary> 轮廓渲染资源模块 </summary>
    public ModuleAssets<Transform> AssetsOutline;
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

    #region 输入模块
    /// <summary> 设计UI输入模块 </summary>
    public ModuleUIInput<UnitMouseInput> UIInputDesign;
    /// <summary> 烘焙UI输入模块 </summary>
    public ModuleUIInput<UnitMouseInput> UIInputBaking;
    #endregion

    #region 转换模块
    /// <summary> 板片预设数据(DataPlatePresets) 转换 板片数据(DataPlate) </summary>
    public ModuleBuilder<DataPlatePresets, DataPlate> PlatePresetsToPlate;
    /// <summary> 板片数据(DataPlate) 转换 多边形数据(DataPolygon) </summary>
    public ModuleBuilder<DataPlate, DataPolygon> PlateToPolygon;
    /// <summary> 插入点(DataInsertPoint) 转换 点(DataPoint) </summary>
    public ModuleBuilder<DataInsertPoint, DataPoint> InsertPointToPoint;
    #endregion

    #region 可视模块
    /// <summary> 设计可视化内容生成模块 </summary>
    public ModuleVisual<DataPlate> VisualDesign;
    /// <summary> 烘焙可视化内容生成模块 </summary>
    public ModuleVisual<DataPlate> VisualBaking;
    /// <summary> 连接可视化内容生成模块 </summary>
    public ModuleVisual<DataConnector> VisualConnector;
    #endregion

    #region 查询模块
    /// <summary> 查询点模块 </summary>
    public ModuleFind<DataPoint> FindPoint;
    /// <summary> 查询边模块 </summary>
    public ModuleFind<DataSide> FindSide;
    /// <summary> 查询贝塞尔点模块 </summary>
    public ModuleFind<DataBezier> FindBezier;
    #endregion

    #region 算法模块
    /// <summary> 计算位置到边上最近的点 </summary>
    public ModuleAlgorithm<DataIntersect> AlgorithmSidePoint;
    /// <summary> 简单多边形算法模块 </summary>
    public ModuleAlgorithm<DataPlate> AlgorithmSimplePolygon;
    /// <summary> 细分多边形算法模块 </summary>
    public ModuleAlgorithm<DataPlate> AlgorithmSubdivisionPolygon;
    /// <summary> 缝合边算法模块 </summary>
    public ModuleAlgorithm<DataSutureSide> AlgorithmSutureSide;
    #endregion

    #region 事件定义
    /// <summary> 标记数据Event </summary>
    public event Action<DataMark> OnMark;
    /// <summary> 移动烘焙视图的板片Event </summary>
    public event Action<DataPlate> OnBakingMobilePlate;
    #endregion

    #region 事件触发
    /// <summary> 触发标记数据Event </summary>
    public void Mark(DataMark data) => OnMark?.Invoke(data);
    /// <summary> 触发移动烘焙视图的板片Event </summary>
    public void BakingMobilePlate(DataPlate data) => OnBakingMobilePlate?.Invoke(data);
    #endregion
}
