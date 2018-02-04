using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour {

    bool isplay = false;

    public void muteMusic()
    {
        AudioSource music = GameObject.Find("MusicOne").GetComponent<AudioSource>();
        music.ignoreListenerVolume = true;

        if (!isplay)
        {
            music.Pause();
            isplay = true;

        }
        else
        {
            music.UnPause();
            isplay = false;
        }
    }

}
