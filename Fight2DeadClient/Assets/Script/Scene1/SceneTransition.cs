using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
     public Animator transitionAnim;
    void Update()
    {
        if (Input.anyKeyDown)
        {
            StartCoroutine(LoadNextScene());
        }
    }

    IEnumerator LoadNextScene()
    {
        transitionAnim.SetTrigger("end");
        yield return new WaitForSeconds(1f);
    }
}