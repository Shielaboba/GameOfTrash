using UnityEngine;

public class MuteSounds : MonoBehaviour {

    bool isPlay = false;

    public void Mute()
    {
        AudioSource sfx = GameObject.Find("BtnSounds").GetComponent<AudioSource>();

        if (!isPlay)
        {
            sfx.volume = 0;
            isPlay = true;
        }
        else
        {
            sfx.volume = 1;
            isPlay = false;
        }
    }
}
