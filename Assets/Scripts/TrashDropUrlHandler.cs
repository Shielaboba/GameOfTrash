using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System;

public class TrashDropUrlHandler : MonoBehaviour {

    Constant c;
    void Start()
    {
        App42Log.SetDebug(true); //Prints output in your editor console
        c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        UploadService uploadService = App42API.BuildUploadService();
        uploadService.GetAllFiles(new TrashDropResponse());        
    }
}

internal class TrashDropResponse : App42CallBack
{
    Texture2D img;
    List<string> urls = new List<string>();
    List<TrashData> trashList = TrashRandomManager.GetInstance().GetTrash();

    public void OnSuccess(object response)
    {
        Upload upload = (Upload)response;
        IList<Upload.File> fileList = upload.GetFileList();

        for (int i = 0; i < fileList.Count; i++)
        {
            for (int j = 0; j < trashList.Count; j++)
            {
                if (trashList[j].TrashName.Equals(fileList[i].GetName()))
                {
                    urls.Add(fileList[i].GetUrl());
                }
                if (trashList.Count == urls.Count) break;
            }
        }
        TrashUrlManager.GetInstance().SetURL(urls);
        Debug.Log("DONE");
    }

    public void OnException(Exception ex)
    {
        Debug.Log(ex.Message);
    }
}
