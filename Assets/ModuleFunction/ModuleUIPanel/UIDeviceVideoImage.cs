using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIDeviceVideoImage : ModuleUIPanel<DataVideoImage> {
    private DataVideoImage data;
    private MediaPlayer mediaPlayer;
    //private VisualElement Background => element.Q<VisualElement>("Background");
    private VisualElement Image => element.Q<VisualElement>("Image");
    private Button Button1 => element.Q<Button>("Button1");
    private Button Button2 => element.Q<Button>("Button2");
    private Button Button3 => element.Q<Button>("Button3");
    private Button CloseButton => element.Q<Button>("Close");

    public override void Awake() {
        //初始化模块
        //ModuleCore.VideoImagePanel = this;
        InitElement();
        //加载视频媒体ui
        VisualElement media = element.Q<VisualElement>("MediaPlayer");
        mediaPlayer = new MediaPlayer(media, MediaPlayerFullScreen);
        //事件绑定
        Button1.clicked += Button1_clicked;
        Button2.clicked += Button2_clicked;
        Button3.clicked += () => { Debug.Log("未定义"); /*FengrenManager.OnAddNum();*/ };
        CloseButton.clicked += Close;
    }
    public override void Open(DataVideoImage data) {
        this.data = data;
        element.style.display = DisplayStyle.Flex;
        //ModuleCore.ModuleVideo.SetValue(data.dataVideo);
        Image.style.backgroundImage = new StyleBackground(data.sprite);
        Button2_clicked();
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
        mediaPlayer.Close();
        Image.style.visibility = Visibility.Visible;
        Button1.style.display = DisplayStyle.None;
        Button2.style.display = DisplayStyle.Flex;
    }
    private void Button2_clicked() {
        mediaPlayer.Open();
        Image.style.visibility = Visibility.Hidden;
        Button1.style.display = DisplayStyle.Flex;
        Button2.style.display = DisplayStyle.None;
    }
}
