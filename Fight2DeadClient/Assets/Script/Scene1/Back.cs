using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip soundEffect;
    public string sceneName;
     public Animator transitionAnim;
    public void Return()
    {
            audioSource.PlayOneShot(soundEffect);
            StartCoroutine(LoadNextScene());
    }

    IEnumerator LoadNextScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(sceneName);
    }
}