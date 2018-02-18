using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System;

public class TrashDropUrlHandler : MonoBehaviour {

    Constant c;
    // public float speed = 10F;
    // Use this for initialization

    List<TrashData> trash;

    void Start()
    {
        trash = TrashRandomManager.GetInstance().GetTrash();
        App42Log.SetDebug(true); //Prints output in your editor console
        c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        UploadService uploadService = App42API.BuildUploadService();

        //for (int i = 0; i < TrashRandomManager.GetInstance().GetTrash().Count; i++)
        //{
            uploadService.GetFileByName("Tire", new TrashDropResponse());
        //    TrashManager.GetInstance().SetTrash(trash[i]);
        //}
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;
            transform.Translate(touchDeltaPosition.x, touchDeltaPosition.y, 0);
        }
    }
}

internal class TrashDropResponse : App42CallBack
{
    public string url;
    Texture2D img;
    TrashData trash;

    public void OnSuccess(object response)
    {
        Upload upload = (Upload)response;
        IList<Upload.File> fileList = upload.GetFileList();

        for (int i = 0; i < fileList.Count; i++)
        {
            //trash = TrashManager.GetInstance().GetTrash();
            //trash.TrashUrl = fileList[i].GetUrl();

            Debug.Log(fileList[i].GetUrl());
            
        }

    }


    public void OnException(Exception ex)
    {
        throw new NotImplementedException();
    }
}
