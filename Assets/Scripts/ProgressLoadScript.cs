﻿using System.Collections;
using System.Collections.Generic;
using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class ProgressLoadScript : MonoBehaviour
{
    String collectionName,key,value,user;
    Constant c;

    public ProgressLoadScript (String user)
    {
        this.user = user;
        c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
    }
    
    public void LoadProgress()
    {
        key = "PlayerName";
        value = user;
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentByKeyValue("GOTDB", "PerformanceFile", key, value, new PerformanceResponse());
    }
    
}
