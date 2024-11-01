using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class DataVideoImage {
    public DataVideo dataVideo;
    public Sprite sprite;
    public DataVideoImage(VideoClip clip, Sprite sprite) {
        this.dataVideo = new DataVideoClip(clip);
        this.sprite = sprite;
    }
}
