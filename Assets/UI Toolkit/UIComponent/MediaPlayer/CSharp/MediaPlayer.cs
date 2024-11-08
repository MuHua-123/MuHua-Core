using System;
using System.Collections;
using System.Collections.Generic;
using MuHua;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class MediaPlayer {
    private bool isDownSlider;
    private float showTime;
    private VisualElement element;
    private Action fullAction;

    private ModuleCore Core => ModuleCore.I;
    //private ModuleVideo ModuleVideo => Core.ModuleVideo;
    //private RenderTexture RenderTexture => ModuleVideo.renderTexture;
    private VisualElement VideoView => element.Q<VisualElement>("VideoView");
    private VisualElement VideoController => element.Q<VisualElement>("VideoController");
    private MUSliderHorizontal Slider => element.Q<MUSliderHorizontal>("Slider");
    private Label Time => element.Q<Label>("Time");
    private Button Play => element.Q<Button>("Play");
    private Button Pause => element.Q<Button>("Pause");
    private Button FullScreen => element.Q<Button>("FullScreen");

    public MediaPlayer(VisualElement element, Action fullAction = null) {
        this.element = element;
        this.fullAction = fullAction;

        Play.clicked += Play_clicked;
        Pause.clicked += Pause_clicked;
        FullScreen.clicked += FullScreen_clicked;

        VideoView.RegisterCallback<PointerDownEvent>((evt) => showTime = 5);
        VideoController.RegisterCallback<PointerDownEvent>((evt) => showTime = 5);

        Slider.RegisterCallback<PointerDownEvent>((evt) => isDownSlider = true);
        Slider.RegisterCallback<PointerUpEvent>((evt) => isDownSlider = false);
        Slider.RegisterCallback<PointerLeaveEvent>((evt) => isDownSlider = false);
        Slider.SlidingValueChanged += Slider_SlidingValueChanged;
    }
    private void Play_clicked() {
        //ModuleVideo.Play(); showTime = 5;
        //Play.style.display = DisplayStyle.None;
        //Pause.style.display = DisplayStyle.Flex;
        //Slider.MaxValue = ModuleVideo.frameCount;
    }
    private void Pause_clicked() {
        //ModuleVideo.Pause();
        //Play.style.display = DisplayStyle.Flex;
        //Pause.style.display = DisplayStyle.None;
    }
    private void FullScreen_clicked() {
        fullAction?.Invoke();
    }
    private void Slider_SlidingValueChanged(float obj) {
        //ModuleVideo.frame = (long)obj;
    }
    public void Open() {
        element.style.visibility = Visibility.Visible;
        //设置渲染纹理
        //Background background = Background.FromRenderTexture(RenderTexture);
        //VideoView.style.backgroundImage = new StyleBackground(background);
        //播放视频
        Play_clicked();
    }
    public void Close() {
        Pause_clicked();
        element.style.visibility = Visibility.Hidden;
    }
    public void Update() {
        showTime -= UnityEngine.Time.deltaTime;
        Visibility visibility = showTime > 0 ? Visibility.Visible : Visibility.Hidden;
        VideoController.style.visibility = visibility;

        //if (!isDownSlider) { Slider.Value = ModuleVideo.frame; }

        //string clockTime = TimeSpan.FromSeconds(ModuleVideo.time).ToString(@"mm\:ss");
        //string length = TimeSpan.FromSeconds(ModuleVideo.maxTime).ToString(@"mm\:ss");
        //Time.text = clockTime + "/" + length;
    }
}
