using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class UIFullScreenVideo : ModuleUIPanel<Action> {
    private Action callback;
    private MediaPlayer mediaPlayer;
    public override void Awake() {
        //ModuleCore.FullScreenVideoPanel = this;
        InitElement();
        VisualElement media = element.Q<VisualElement>("MediaPlayer");
        mediaPlayer = new MediaPlayer(media, Close);
    }
    public override void Open(Action data) {
        callback = data;
        element.style.display = DisplayStyle.Flex;
        mediaPlayer.Open();
    }
    public override void Close() {
        mediaPlayer.Close();
        element.style.display = DisplayStyle.None;
        callback?.Invoke();
    }

    private void LateUpdate() {
        bool isDisplay = element.resolvedStyle.display == DisplayStyle.Flex;
        if (isDisplay) { mediaPlayer.Update(); }
    }
}
