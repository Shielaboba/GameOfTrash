﻿using System;
using System.Collections;
using System.Collections.Generic;   
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.UI;


public class TriviaResponse : App42CallBack
{
    public GameObject window;
    public Text DisplayName;
    public Text DisplayDesc;
   
   // Boolean flag;
    

    void Init()
    {
        DisplayDesc = GameObject.Find("displayDesc").GetComponent<Text>();
        DisplayName = GameObject.Find("displayName").GetComponent<Text>();
        
    }

    public void OnSuccess(object response)
    {

       Init();
        Storage storage = (Storage)response;

           IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();

           TrashData trash = new TrashData();
 
               trash = JsonUtility.FromJson<TrashData>(jsonDocList[0].GetJsonDoc());

               DisplayName.text = trash.TrashName.ToUpper();
               DisplayDesc.text = trash.TrashTrivia;

      // window.SetActive(true);
       App42Log.SetDebug(true);
    }
    public void OnException(Exception e)
    {
       // Debug.Log("booo");
        App42Log.Console("Exception : " + e);
        App42Log.SetDebug(true);
    }
}
