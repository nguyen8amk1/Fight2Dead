using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManage : MonoBehaviour
{
    public VideoPlayer videoPlayer1;
    public VideoPlayer videoPlayer2;
    public GameObject cube1;
    public GameObject cube2;
    public float transitionDuration = 1f; 

    private void Start()
    {
        videoPlayer1.loopPointReached += OnVideo1End;
    }

    private void OnVideo1End(VideoPlayer vp)
    {
        StartCoroutine(TransitionVideos());
    }

    IEnumerator TransitionVideos()
    {
        
        float timer = 0f;
        while (timer < transitionDuration)
        {
            float t = timer / transitionDuration;

           
            videoPlayer1.targetCameraAlpha = 1f - t;

           
            videoPlayer2.targetCameraAlpha = t;

            timer += Time.deltaTime;
            yield return null;
        }

       
        videoPlayer1.gameObject.SetActive(false);
        cube1.SetActive(false);
        videoPlayer2.gameObject.SetActive(true);
        cube2.SetActive(true);

        
        videoPlayer1.targetCameraAlpha = 1f;
        videoPlayer2.targetCameraAlpha = 1f;
    }
}
