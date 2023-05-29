using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
public class AudioController : MonoBehaviour
{
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    private void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

       
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
        videoPlayer.controlledAudioTrackCount = 1;
        videoPlayer.SetTargetAudioSource(0, audioSource);

        
        videoPlayer.Play();
        audioSource.Play();
    }
}
