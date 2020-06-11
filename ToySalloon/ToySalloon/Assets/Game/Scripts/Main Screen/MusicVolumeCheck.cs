using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicVolumeCheck : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void  UpdateVolume()
    {
        this.GetComponent<AudioSource>().volume = PlayerPrefs.GetFloat("MusicVolume");
    }
}
