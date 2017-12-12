using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp.user;
using System;

public class MapScript: MonoBehaviour {

    public int level;
    public Sprite level1;
    public Sprite level2;
    public Sprite level3;
    public Sprite level4;
    public Sprite level5;
    public GameObject bg;

    MapScript (int level)
    {
        this.level = level;
    }

    void Start()
    {
        switch (level)
        {
            case 1: bg.GetComponent<Image>().sprite = level1; break;
            case 2: bg.GetComponent<Image>().sprite = level2; break;
            case 3: bg.GetComponent<Image>().sprite = level3; break;
            case 4: bg.GetComponent<Image>().sprite = level4; break;
            case 5: bg.GetComponent<Image>().sprite = level5; break;
        }
    }

}
