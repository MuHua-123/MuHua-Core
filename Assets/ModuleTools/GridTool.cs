using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridTool<Data> {
    public Data[,] array;
    public readonly int wide;
    public readonly int high;
    /// <summary> 初始化网格 </summary>
    public GridTool(int wide, int high, Func<int, int, Data> generate) {
        this.wide = wide;
        this.high = high;
        array = new Data[wide, high];
        Loop((x, y) => { array[x, y] = generate(x, y); });
    }
    /// <summary> 循环网格获取x和y </summary>
    public void Loop(Action<int, int> action) {
        for (int y = 0; y < high; y++) {
            for (int x = 0; x < wide; x++) { action?.Invoke(x, y); }
        }
    }
    /// <summary> 校验xy是否超限 </summary>
    public bool TryXY(int x, int y) {
        return x >= 0 && x < wide && y >= 0 && y < high;
    }
    /// <summary> 强制写入数据，超过边界时写在边界 </summary>
    public Data Get(int x, int y) {
        x = Mathf.Clamp(x, 0, wide - 1);
        y = Mathf.Clamp(y, 0, high - 1);
        return array[x, y];
    }
    /// <summary> 强制读取数据，超过边界时读取边界 </summary>
    public void Set(int x, int y, Data data) {
        x = Mathf.Clamp(x, 0, wide - 1);
        y = Mathf.Clamp(y, 0, high - 1);
        array[x, y] = data;
    }
    /// <summary> 校验xy是否超限 读取正确范围内的数据 </summary>
    public bool TryGet(int x, int y, out Data data) {
        data = Get(x, y); return TryXY(x, y);
    }
    /// <summary> 校验xy是否超限 写入正确范围内的数据 </summary>
    public bool TrySet(int x, int y, Data data) {
        if (TryXY(x, y)) { array[x, y] = data; return true; }
        else { return false; }
    }
}
