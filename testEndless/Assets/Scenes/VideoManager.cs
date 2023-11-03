using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class VideoManager : MonoBehaviour
{
    public TextMeshProUGUI currentTime, maxTime, levelTitle;
    public VideoPlayer vidPlayer;

    public string[] videoName = {
        "bab1Comp.mp4",
        "bab2Comp.mp4",
        "bab3Comp.mp4",
        "bab4Comp.mp4",
        "bab5Comp.mp4",
    };

    public RenderTexture renderTextureMain;
    public RawImage screen;

    public float volume;

    public Button playBtn, pauseBtn;
    public Slider volumeSlider, videoSlider;

    public Sprite playImg, pauseImg;
    public bool isPlaying;

    private int videoIndex;

    public bool press;

    private void Awake()
    {
        vidPlayer.playOnAwake = false;
        
        videoIndex = GameManager.instance.selectedBab;
        if(vidPlayer.targetTexture != null)
        {
            vidPlayer.targetTexture.Release();
        }
        else
        {
            vidPlayer.targetTexture = new RenderTexture(1920, 1080, 24);
        }

        screen.texture = vidPlayer.targetTexture;
        string url = System.IO.Path.Combine(Application.streamingAssetsPath + "/Video/" + videoName[videoIndex]);
        vidPlayer.source = VideoSource.Url;
        vidPlayer.url = url;


        Debug.Log(url);
    }

    // Start is called before the first frame update
    void Start()
    {
        levelTitle.text = GameManager.instance.customTitle;
        AudioManager.instance.Pause(AudioManager.instance.GetCurrentlyPlayingAudio());
        press = false;
        

        volumeSlider.value = volumeSlider.maxValue;
        CheckVolume();
        
    }

    private void Update()
    {
        CheckVolume();
        CheckVideoSlider();
    }

    public void CheckVideoSlider()
    {
        videoSlider.value = Mathf.InverseLerp(0, (float)vidPlayer.length, (float)vidPlayer.time);
        currentTime.text = ConvertTime(vidPlayer.time);
        maxTime.text = ConvertTime(vidPlayer.length);
    }

    public void CheckVolume()
    {
        isPlaying = vidPlayer.isPlaying;
        vidPlayer.SetDirectAudioVolume(0, volumeSlider.value);
        
    }

    public string ConvertTime(double time)
    {
        int hour, minute, sec;
        
        hour = (int)time / 3600;
        minute = ((int)time - (hour * 3600)) / 60;
        sec = (int)time % 60;


        return
            hour > 0 ? ((hour < 10 ? "0" + hour : hour.ToString()) + ":") : "" 
            + (minute < 10 ? "0" + minute : minute.ToString()) + ":"
            + (sec < 10 ? "0" + sec : sec.ToString());
        
    }

    public void Play()
    {
        Image imgBtn = playBtn.transform.GetChild(0).GetComponent<Image>();

        press = !press;

        if (press)
        {
            vidPlayer.Play();
            imgBtn.sprite = pauseImg;
        }
        else
        {
            vidPlayer.Pause();
            imgBtn.sprite = playImg;
        }

    }
    

    public void Replay()
    {
        Image imgBtn = playBtn.transform.GetChild(0).GetComponent<Image>();
        vidPlayer.Stop();

        if (press)
        {
            imgBtn.sprite = playImg;
        }
       
        press = false;
    }


}
