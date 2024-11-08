using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Video;
using MuHua;

public class UILearningVideo : ModuleUIPanel<Action> {
    [CustomLabel("避色差排料")]
    public VideoClip clip1;
    [CustomLabel("混合排料")]
    public VideoClip clip2;
    [CustomLabel("女西服排料方法讲解")]
    public VideoClip clip3;
    private Action data;
    private MediaPlayer mediaPlayer;
    private Button Button1 => element.Q<Button>("Button1");
    private Button Button2 => element.Q<Button>("Button2");
    private Button Button3 => element.Q<Button>("Button3");
    private Button CloseButton => element.Q<Button>("Close");
    public override void Awake() {
        //初始化模块
        //ModuleCore.LearningVideoPanel = this;
        InitElement();
        //加载视频媒体ui
        VisualElement media = element.Q<VisualElement>("MediaPlayer");
        mediaPlayer = new MediaPlayer(media, MediaPlayerFullScreen);
        //事件绑定
        Button1.clicked += Button1_clicked;
        Button2.clicked += Button2_clicked;
        Button3.clicked += Button3_clicked;
        CloseButton.clicked += Close;
    }
    public override void Open(Action data) {
        this.data = data;
        element.style.display = DisplayStyle.Flex;
        Button1_clicked();
    }
    public override void Close() {
        mediaPlayer.Close();
        element.style.display = DisplayStyle.None;
    }

    private void LateUpdate() {
        bool isDisplay = element.resolvedStyle.display == DisplayStyle.Flex;
        if (isDisplay) { mediaPlayer.Update(); }
    }
    /// <summary> 全屏播放功能 </summary>
    private void MediaPlayerFullScreen() {
        element.style.display = DisplayStyle.None;
        //ModuleCore.FullScreenVideoPanel.Open(() => {
        //    mediaPlayer.Open();
        //    element.style.display = DisplayStyle.Flex;
        //});
    }
    private void Button1_clicked() {
        DataVideoClip videoClip = new DataVideoClip(clip1);
        //ModuleCore.ModuleVideo.SetValue(videoClip);
        mediaPlayer.Open();
    }
    private void Button2_clicked() {
        DataVideoClip videoClip = new DataVideoClip(clip2);
        //ModuleCore.ModuleVideo.SetValue(videoClip);
        mediaPlayer.Open();
    }
    private void Button3_clicked() {
        DataVideoClip videoClip = new DataVideoClip(clip3);
        //ModuleCore.ModuleVideo.SetValue(videoClip);
        mediaPlayer.Open();
    }
}
