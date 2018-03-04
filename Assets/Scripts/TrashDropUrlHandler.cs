using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System;

public class TrashDropUrlHandler : MonoBehaviour {

    void Start()
    {
        Constant c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        UploadService uploadService = App42API.BuildUploadService();
        uploadService.GetAllFiles(new TrashDropResponse());        
    }
}

internal class TrashDropResponse : App42CallBack
{
    Texture2D img;
    int counter = 0;
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
                    trashList[j].TrashUrl = fileList[i].GetUrl();
                    counter++;
                }
                if (trashList.Count == counter) break;
            }
        }
        TrashRandomManager.GetInstance().SetTrash(trashList);
    }

    public void OnException(Exception ex)
    {
        Debug.Log(ex.Message);
    }
}
