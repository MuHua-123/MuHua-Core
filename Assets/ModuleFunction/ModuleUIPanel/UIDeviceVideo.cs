using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDeviceVideo : ModuleUIPanel<DataVideo> {
    private DataVideo data;
    private MediaPlayer mediaPlayer;
    private Button CloseButton => element.Q<Button>("Close");
    public override void Awake() {
        //初始化模块
        //ModuleCore.VideoPanel = this;
        InitElement();
        //加载视频媒体ui
        VisualElement media = element.Q<VisualElement>("MediaPlayer");
        mediaPlayer = new MediaPlayer(media, MediaPlayerFullScreen);
        //事件绑定
        CloseButton.clicked += Close;
    }
    public override void Open(DataVideo data) {
        this.data = data;
        element.style.display = DisplayStyle.Flex;
        //ModuleCore.ModuleVideo.SetValue(data);
        mediaPlayer.Open();
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
}
