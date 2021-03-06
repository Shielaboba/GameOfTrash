﻿using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System;
using UnityEngine.UI;

public class TrashDropUrlHandler : MonoBehaviour {
    GameObject tutorialPanel;
    Button OkBtn;

    void Start()
    {
        tutorialPanel = GameObject.Find("TutorialPanel");
        OkBtn = GameObject.Find("OkBtn").GetComponent<Button>();

        if (PlayerManager.GetInstance().GetPlayer().PlayerGameLvlNo == 1)
        {
            OkBtn.onClick.AddListener(delegate () {
                tutorialPanel.SetActive(false);
            });
        }
        else tutorialPanel.SetActive(false);

        Constant c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        UploadService uploadService = App42API.BuildUploadService();
        uploadService.GetAllFiles(new TrashDropResponse());        
    }
}

internal class TrashDropResponse : App42CallBack
{
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
