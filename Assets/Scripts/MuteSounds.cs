using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteSounds : MonoBehaviour {
    bool isplay = false;
    public void muteSounds()
    {
        AudioSource sfx = GameObject.Find("BtnSounds").GetComponent<AudioSource>();

        if (!isplay)
        {
            sfx.volume = 0;
            isplay = true;

        }
        else
        {
            sfx.volume = 1;
            isplay = false;
        }
    }
}
