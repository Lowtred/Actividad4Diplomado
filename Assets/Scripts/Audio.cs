using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip[] audios;
    public AudioSource audioSource;
    void Start()
    {
        audioSource.PlayOneShot(audios[0]);
    }


}
