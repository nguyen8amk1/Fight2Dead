using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyParticle : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource switchSound;
    void Start()
    {
        switchSound.Play();
        StartCoroutine(DelayedDestroy(0.35f));
    }

    IEnumerator DelayedDestroy(float delay)
    {
        yield return new WaitForSeconds(delay);

        Destroy(gameObject);
    }
}
