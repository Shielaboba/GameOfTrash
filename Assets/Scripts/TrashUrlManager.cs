using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashUrlManager {

    private static TrashUrlManager Instance;
    private List<string> url;
    private TrashUrlManager()
    {
        //CurrentTrash = "";
    }

    public static TrashUrlManager GetInstance()
    {
        if (Instance != null)
        {
            return Instance;
        }
        else
        {
            Instance = new TrashUrlManager();
            return Instance;
        }
    }

    public List<string> GetURL() { return url; }
    public void SetURL(List<string> url) { this.url = url; }
}
