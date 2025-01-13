using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LoopIndexTool {
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
        if (maxIndex == 0) { Debug.LogError("错误索引：maxIndex = 0"); return 0; }
        if (index < 0) { return LoopIndex(index + maxIndex, maxIndex); }
        if (index >= maxIndex) { return LoopIndex(index - maxIndex, maxIndex); }
        return index;
    }
}
