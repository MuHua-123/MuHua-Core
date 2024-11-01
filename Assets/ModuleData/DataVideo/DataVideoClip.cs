using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DataVideoClip : DataVideo {
    public readonly VideoClip videoClip;
    public DataVideoClip(VideoClip videoClip) {
        this.videoClip = videoClip;
    }
    public override void SetPlayer(VideoPlayer videoPlayer) {
        videoPlayer.source = VideoSource.VideoClip;
        videoPlayer.clip = videoClip;
    }
    public static List<DataVideoClip> ToData(List<VideoClip> list) {
        List<DataVideoClip> dataVideoClips = new List<DataVideoClip>();
        list.ForEach(obj => dataVideoClips.Add(new DataVideoClip(obj)));
        return dataVideoClips;
    }
}
