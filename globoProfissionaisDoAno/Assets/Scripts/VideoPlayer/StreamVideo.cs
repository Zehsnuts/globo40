using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StreamVideo : MonoBehaviour
{
    private string url;
    public RawImage image;

    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    private AudioSource audioSource;

    private bool isFullScreen;

    public GameObject appUI;


    public Text currentMinutes;
    public Text currentSeconds;
    public Text totalMinutes;
    public Text totalSeconds;

    public float barValue;


    // Use this for initialization
    void Start()
    {
        Application.runInBackground = true;
        //Add VideoPlayer to the GameObject
        videoPlayer = transform.GetComponent<VideoPlayer>();
        //videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

        StartCoroutine(playVideo());
    }

    IEnumerator playVideo()
    {
        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;
        audioSource.Pause();

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;

        //Vide clip from Url
        //videoPlayer.source = VideoSource.Url;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        videoPlayer.aspectRatio = VideoAspectRatio.FitVertically;

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared)
        {
            yield return null;
        }

        Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        image.texture = videoPlayer.texture;

        //Set Video Total Timer
        SetTotalTimeUI();

        //Play Video
        //videoPlayer.Play();

        //Play Sound
        //audioSource.Play();

        //Debug.Log("Playing Video");
    }

    private void Update()
    {
        if (videoPlayer.isPlaying)
        {
            SetCurrentTimeUI();

            MovePlayhead(CalculatePlayedFraction());
        }
    }

    public void PauseVideo()
    {
        //Play Video
        videoPlayer.Pause();

        //Play Sound
        audioSource.Pause();
    }

    public void PlayVideo()
    {
        if(FindObjectOfType<VideoInfoScreen>())
            FindObjectOfType<VideoInfoScreen>().HideThumbnail();

        //Play Video
        if (videoPlayer.isPlaying)
        {
            videoPlayer.Pause();
            audioSource.Pause();
        }
        else
        {
            videoPlayer.Play();
            audioSource.Play();
        }        
    }

    public void GoFullScreen()
    {
        isFullScreen = true;
        appUI.SetActive(false);
    }

    public void ExitFullScreen()
    {
        isFullScreen = false;
        appUI.SetActive(true);
    }

    void SetCurrentTimeUI()
    {
        string minutes = Mathf.Floor((int)videoPlayer.time / 60).ToString("00");
        string seconds = ((int)videoPlayer.time % 60).ToString("00");

        currentMinutes.text = minutes;
        currentSeconds.text = seconds;
    }

    void SetTotalTimeUI()
    {
        string minutes = Mathf.Floor((int)videoPlayer.clip.length / 60).ToString("00");
        string seconds = ((int)videoPlayer.clip.length % 60).ToString("00");

        totalMinutes.text = minutes;
        totalSeconds.text = seconds;
    }

    public Transform startPoint;
    public Transform endPoint;
    public Transform playHead;

    public void UpdateVideoPosition(float vl)
    {
        Debug.Log(barValue + " / " + vl);
    }

    public void MovePlayhead(double playedFraction)
    {
        playHead.position = Vector2.Lerp(startPoint.position, endPoint.position, (float)playedFraction);
    }

    double CalculatePlayedFraction()
    {
        double fraction = (double)videoPlayer.frame / (double)videoPlayer.clip.frameCount;
        return fraction;
    }

    public void ChangeVideo(string hash)
    {
        //if !UNITY_EDITOR && UNITY_ANDROID
        //url = Application.streamingAssetsPath + "Videos/" + "1_LANÇAMENTO DO ANO_A SUA RECEITA.mp4";

        //string url = "file://" + Application.streamingAssetsPath + "Videos/" + "1_LANÇAMENTO DO ANO_A SUA RECEITA.mp4";

        VideoClip clip = Resources.Load<VideoClip>("Videos/"+ hash) as VideoClip;

        videoPlayer.clip = videoToPlay = clip;

        videoPlayer.Stop();

        if (clip != null)
        {
            MovePlayhead(CalculatePlayedFraction());
            SetCurrentTimeUI();
            SetTotalTimeUI();
            StartCoroutine(playVideo());
        }
    }
}