using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;
using TMPro;

public class VideoManager : MonoBehaviour
{
    public TextMeshProUGUI currentTime, maxTime;
    public VideoClip[] videos;
    public VideoPlayer vidPlayer;
    public RenderTexture renderTextureMain;
    public RawImage screen;

    public float volume;

    public Button playBtn, pauseBtn;
    public Slider volumeSlider, videoSlider;

    public Sprite playImg, pauseImg;
    public bool isPlaying;

    private int videoIndex;

    private bool press;

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
            vidPlayer.targetTexture = new RenderTexture((int)videos[videoIndex].width, (int)videos[videoIndex].height, 24);
        }

        screen.texture = vidPlayer.targetTexture;
        vidPlayer.clip = videos[videoIndex];
        

    }

    // Start is called before the first frame update
    void Start()
    {
        press = false;

        Debug.Log(videos[videoIndex].length);

        volumeSlider.value = volumeSlider.maxValue;
        CheckVolume();
        Play();
        //StartCoroutine(ResetThumbnail());
        
    }

    private void Update()
    {
        CheckVolume();
    }


    public void CheckVolume()
    {
        isPlaying = vidPlayer.isPlaying;
        vidPlayer.SetDirectAudioVolume(0, volumeSlider.value);

        if (!press)
        {
            videoSlider.value = Mathf.InverseLerp(0, (float)vidPlayer.length, (float)vidPlayer.time);
            currentTime.text = ConvertTime(vidPlayer.time);
        }
        else
        {
            
            //currentTime.text = ConvertTime((double)Mathf.Lerp(0,1,videoSlider.value));
        }
        currentTime.text = ConvertTime(vidPlayer.time);
        maxTime.text = ConvertTime(vidPlayer.length);

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
        press = !press;
        Image imgBtn = playBtn.transform.GetChild(0).GetComponent<Image>();
        if (press)
        {
            Debug.Log(imgBtn.rectTransform.localPosition);
            imgBtn.rectTransform.localPosition = new Vector2(5, 2);
            
            vidPlayer.Pause();
            imgBtn.sprite = playImg;
            Debug.Log("VideoPlayed");
        }
        else
        {
            //vidPlayer.time = Mathf.Lerp(0, 1, (float)vidPlayer.time);
            imgBtn.sprite = pauseImg;
            imgBtn.rectTransform.localPosition = new Vector2(0, 0);
            vidPlayer.Play();

            
            Debug.Log("VideoPaused");
        }
        
    }
    

    public void Replay()
    {
        vidPlayer.Stop();
        Play();
        videoSlider.value = 0;
    }


}
