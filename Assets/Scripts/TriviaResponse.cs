using System;
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
    Text btnText;    

    void Init()
    {
        DisplayDesc = GameObject.Find("displayDesc").GetComponent<Text>();
        DisplayName = GameObject.Find("displayName").GetComponent<Text>();
        btnText = GameObject.Find("BtnDiy").GetComponentInChildren<Text>();
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
               btnText.text = "Do It Yourself!";
        App42Log.SetDebug(true);
    }
    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
        App42Log.SetDebug(true);
    }
}
