using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DataVideoWeb : DataVideo {
    public readonly string url;
    public DataVideoWeb(string url) {
        this.url = url;
    }
    public override void SetPlayer(VideoPlayer videoPlayer) {
        videoPlayer.source = VideoSource.Url;
        videoPlayer.url = url;
    }
    public static List<DataVideoWeb> ToData(List<string> list) {
        List<DataVideoWeb> dataVideoWebs = new List<DataVideoWeb>();
        list.ForEach(obj => dataVideoWebs.Add(new DataVideoWeb(obj)));
        return dataVideoWebs;
    }
}
