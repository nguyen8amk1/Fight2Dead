using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManage : MonoBehaviour
{
    public VideoPlayer[] videoPlayers;
    private int currentVideoIndex = 0;

    private void Start()
    {
        PlayCurrentVideo();
    }

    private void PlayCurrentVideo()
    {
        // Ensure the currentVideoIndex is within bounds
        if (currentVideoIndex < 0 || currentVideoIndex >= videoPlayers.Length)
        {
            Debug.Log("Invalid current video index");
            return;
        }

        // Get the reference to the VideoPlayer component for the current video
        VideoPlayer currentVideoPlayer = videoPlayers[currentVideoIndex];

        // Subscribe to the current videoPlayer loopPointReached event
        currentVideoPlayer.loopPointReached += OnVideoFinished;

        // Play the current video
        currentVideoPlayer.Play();
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        // Video has finished playing
        Debug.Log("Video finished playing");

        // Unsubscribe from the current videoPlayer loopPointReached event
        vp.loopPointReached -= OnVideoFinished;

        // Move to the next video
        currentVideoIndex++;

        // Check if all videos have been played
        if (currentVideoIndex >= videoPlayers.Length)
        {
            Debug.Log("All videos finished playing");
            return;
        }

        // Play the next video
        PlayCurrentVideo();
    }
}
