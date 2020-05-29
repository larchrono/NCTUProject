using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class YoutubeManager : SoraLib.SingletonMono<YoutubeManager>
{
    public string currentURL;
    protected VideoPlayer videoPlayer;
    protected YoutubePlayer player;

    protected override void OnSingletonAwake()
    {
        videoPlayer = GetComponentInChildren<VideoPlayer>();
        player = GetComponentInChildren<YoutubePlayer>();
        player.videoPlayer = videoPlayer;
        player.audioPlayer = videoPlayer;
        player.videoQuality = YoutubePlayer.YoutubeVideoQuality.STANDARD;
    }

    public void Play(){
        if(string.IsNullOrEmpty(currentURL))
            return;
        
        player.Play(currentURL);
    }

    public void Play(string url){
        if(string.IsNullOrEmpty(url))
            return;

        currentURL = url;
        player.Play(url);
    }

    public void Stop(){
        player.Stop();
    }

    public bool CanPlay(){
        if(string.IsNullOrEmpty(currentURL))
            return false;

        return true;
    }
}
