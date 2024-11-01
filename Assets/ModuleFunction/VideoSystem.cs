using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class VideoSystem : ModuleVideo {
    public Vector2Int renderSize = new Vector2Int(1920, 1080);

    private int index;
    private RenderTexture rTexture;
    private List<DataVideo> videoDatas = new List<DataVideo>();
    private VideoPlayer videoPlayer => GetComponent<VideoPlayer>();

    protected override void Awake() {
        base.Awake();
        rTexture = new RenderTexture(renderSize.x, renderSize.y, 0);
        videoPlayer.targetTexture = rTexture;
    }
    protected override bool IsPlaying() => videoPlayer.isPlaying;
    protected override Vector2Int VideoCount() => new Vector2Int(index, videoDatas.Count);
    protected override RenderTexture RenderTexture() => rTexture;
    protected override double Time() => videoPlayer.clockTime;
    protected override double MaxTime() => videoPlayer.length;
    protected override long GetFrame() => videoPlayer.frame;
    protected override void SetFrame(long value) => videoPlayer.frame = value;
    protected override ulong FrameCount() => videoPlayer.frameCount;

    public override void Play() {
        videoDatas[index].SetPlayer(videoPlayer);
        videoPlayer.Play();
    }
    public override void Pause() {
        videoPlayer.Pause();
    }
    public override void Stop() {
        videoPlayer.Stop();
    }
    public override void SetIndex(int value) {
        if (videoDatas.Count == 0) { Debug.LogError("没有视频可以播放！"); Stop(); return; }
        if (value < 0) { value = videoDatas.Count - 1; }
        if (value >= videoDatas.Count) { value = 0; }
        index = value; Play();
    }
    public override void AddIndex(int value) {
        SetIndex(index + value);
    }

    public override void SetValue(DataVideo value) {
        index = 0;
        videoDatas = new List<DataVideo> { value };
    }
    public override void SetValue(List<DataVideo> list) {
        index = 0;
        videoDatas = list;
    }
}
