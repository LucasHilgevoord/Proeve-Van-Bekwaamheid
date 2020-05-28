using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAudio : MonoBehaviour
{
    public AudioSource source;

    private void Awake()
    {
        if(source == null)
        {
            source = Camera.main.GetComponent<AudioSource>();
        }
    }

    public void Play(AudioClip clip)
    {
        source.PlayOneShot(clip);
    }
}
