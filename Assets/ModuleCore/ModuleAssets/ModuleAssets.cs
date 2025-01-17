using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 资源模块
/// </summary>
public class ModuleAssets<Data> {
    protected List<Data> datas = new List<Data>();
    /// <summary> 核心模块 </summary>
    protected virtual ModuleCore ModuleCore => ModuleCore.I;

    /// <summary> 更改事件 </summary>
    public virtual event Action OnChange;
    /// <summary> 数据列表 </summary>
    public virtual List<Data> Datas => datas;
    /// <summary> 数据计数 </summary>
    public virtual int Count => Datas.Count;
    /// <summary> 数据计数 </summary
    public virtual Data this[int index] => Datas[index];

    /// <summary> 添加数据 </summary>
    public virtual void Add(Data data) { Datas.Add(data); OnChange?.Invoke(); }
    /// <summary> 添加数据 </summary>
    public virtual void AddRange(IList<Data> data) { Datas.AddRange(data); OnChange?.Invoke(); }
    /// <summary> 删除数据 </summary>
    public virtual void Remove(Data data) { Datas.Remove(data); OnChange?.Invoke(); }

    /// <summary> 保存数据 </summary>
    public virtual void Save() { throw new NotImplementedException(); }
    /// <summary> 加载数据 </summary>
    public virtual void Load() { throw new NotImplementedException(); }

    /// <summary> 循环列表 </summary>
    public virtual void ForEach(Action<Data> action) => Datas.ForEach(action);
}
/// <summary>
/// 资源模块工具
/// </summary>
public static class ModuleAssetsTool {
    /// <summary> 头尾循环标准化索引 </summary>
    public static Data LoopIndex<Data>(this ModuleAssets<Data> assets, int index) {
        return assets[LoopIndex(index, assets.Count)];
    }
    /// <summary> 头尾循环标准化索引 </summary>
    public static Data LoopIndex<Data>(this List<Data> list, int index) {
        return list[LoopIndex(index, list.Count)];
    }
    /// <summary> 头尾循环标准化索引 </summary>
    public static Data LoopIndex<Data>(this Data[] array, int index) {
        return array[LoopIndex(index, array.Length)];
    }
    /// <summary> 头尾循环标准化索引 </summary>
    public static int LoopIndex(int index, int maxIndex) {
        return index % maxIndex;
    }
}