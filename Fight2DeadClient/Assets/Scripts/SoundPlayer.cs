using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioSource soundPlayer;  
    public void PlaySound()
    {
        soundPlayer.Play();
    }    
}
