using UnityEngine;

public class BgAudio : MonoBehaviour {

    public AudioSource music;
    private static BgAudio instance = null;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(gameObject);
    }

    public static BgAudio Instance
    {
        get { return instance; }
    }
    
}
