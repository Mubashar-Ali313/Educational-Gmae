using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnablingSound : MonoBehaviour
{

    public AudioSource audioSource;    // Audio Source component
    public AudioClip clickSound;       // Sound jo bajani hai



    // Start is called before the first frame update
    void Start()
    {
        if(clickSound != null)
        {
            PlaySound();
        }
    }

    void PlaySound()
    {
        if (audioSource != null && clickSound != null)
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
