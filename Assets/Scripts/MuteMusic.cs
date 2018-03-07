using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuteMusic : MonoBehaviour {

    bool isPlay = false;

    public void Mute()
    {
        AudioSource music = GameObject.Find("MusicOne").GetComponent<AudioSource>();
        music.ignoreListenerVolume = true;

        if (!isPlay)
        {
            music.Pause();
            isPlay = true;
        }
        else
        {
            music.UnPause();
            isPlay = false;
        }
    }
}
