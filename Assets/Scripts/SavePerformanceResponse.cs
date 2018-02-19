using com.shephertz.app42.paas.sdk.csharp.storage;
using com.shephertz.app42.paas.sdk.csharp;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class SavePerformanceResponse : App42CallBack
{
    public void OnSuccess(object response)
    {
        Storage storage = (Storage)response;
        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
        for (int i = 0; i < jsonDocList.Count; i++)
        {
            App42Log.Console("objectId is " + jsonDocList[i].GetDocId());
            App42Log.Console("Created At " + jsonDocList[i].GetCreatedAt());
        }
    }
    public void OnException(Exception e)
    {
        App42Log.Console("Exception : " + e);
    }
}
