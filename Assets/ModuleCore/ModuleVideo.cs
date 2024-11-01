using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public abstract class ModuleVideo : MonoBehaviour {
    protected virtual ModuleCore Core => ModuleCore.I;
    protected virtual void Awake() => Core.FunctionRegister(this);

    /// <summary> 视频播放状态 </summary>
    public virtual bool isPlaying => IsPlaying();
    /// <summary> 从0开始的视频计数，x = 当前索引，y = 最大索引 </summary>
    public virtual Vector2Int videoCount => VideoCount();
    /// <summary> 视频渲染纹理 </summary>
    public virtual RenderTexture renderTexture => RenderTexture();
    /// <summary> 当前视频播放时间 </summary>
    public virtual double time => Time();
    /// <summary> 最大视频播放时间 </summary>
    public virtual double maxTime => MaxTime();
    /// <summary> 当前播放帧 </summary>
    public virtual long frame { get => GetFrame(); set => SetFrame(value); }
    /// <summary> 最大播放帧 </summary>
    public virtual ulong frameCount => FrameCount();

    /// <summary> 视频播放状态 </summary>
    protected abstract bool IsPlaying();
    /// <summary> 从0开始的视频计数，x = 当前索引，y = 最大索引 </summary>
    protected abstract Vector2Int VideoCount();
    /// <summary> 视频渲染纹理 </summary>
    protected abstract RenderTexture RenderTexture();
    /// <summary> 当前视频播放时间 </summary>
    protected abstract double Time();
    /// <summary> 最大视频播放时间 </summary>
    protected abstract double MaxTime();
    /// <summary> get当前播放帧 </summary>
    protected abstract long GetFrame();
    /// <summary> set当前播放帧 </summary>
    protected abstract void SetFrame(long value);
    /// <summary> 最大播放帧 </summary>
    protected abstract ulong FrameCount();

    /// <summary> 播放视频 </summary>
    public abstract void Play();
    /// <summary> 暂停视频 </summary>
    public abstract void Pause();
    /// <summary> 停止视频 </summary>
    public abstract void Stop();
    /// <summary> 根据索引播放视频 </summary>
    public abstract void SetIndex(int value);
    /// <summary> 根据累加的索引播放视频 </summary>
    public abstract void AddIndex(int value);

    /// <summary> 设置视频数据 </summary>
    public abstract void SetValue(DataVideo value);
    /// <summary> 设置视频数据列表 </summary>
    public abstract void SetValue(List<DataVideo> list);

    /// <summary> 设置视频数据，并且播放 </summary>
    public virtual void Play(DataVideo value) { SetValue(value); Play(); }
    /// <summary> 设置视频数据列表，并且第一个播放index位置的视频数据 </summary>
    public virtual void Play(List<DataVideo> list, int index = 0) { SetValue(list); SetIndex(index); }
}