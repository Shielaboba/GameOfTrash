using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BgAudio : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
    public Slider volMusic;
    public AudioSource music;
    private static BgAudio instance = null;
    public static BgAudio Instance
    {
        get { return instance;  }
    }

    void Awake()
    {
        if (instance!=null && instance!=this)
        {
            Destroy(this.gameObject);
            return;
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(this.gameObject);
    }

    void Update()
    {
        music.volume = volMusic.value;
    }
}
