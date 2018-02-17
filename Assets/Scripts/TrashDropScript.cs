using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System;

public class TrashDropScript : MonoBehaviour {

    Constant c;
    // public float speed = 10F;
    // Use this for initialization
    
    String fileName = "Tire";

    void Start () {
        
        App42Log.SetDebug(true); //Prints output in your editor console
        c = new Constant();
        App42API.Initialize(c.apiKey, c.secretKey);
        UploadService uploadService = App42API.BuildUploadService();
        uploadService.GetFileByName(fileName, new TrashDropResponse());
        //uploadService.GetAllFiles(new TrashDropResponse());

        //TrashDropResponse tdr = new TrashDropResponse();
        //tdr.LoadImg(); //
        //StartCoroutine(tdr.LoadImg());
    }

    // Update is called once per frame
    void Update () {

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;            
			transform.Translate(touchDeltaPosition.x, touchDeltaPosition.y, 0);
		}

		/*
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				Vector3 touchedPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x,touch.position.y,10));
				transform.position = Vector3.Lerp (transform.position,touchedPos,Time.deltaTime);
			}
		}
		*/
	}
}

internal class TrashDropResponse : App42CallBack
{
    public string url;
    Texture2D img;
    public void OnSuccess(object response)
    {
        Upload upload = (Upload) response;
        IList<Upload.File>  fileList = upload.GetFileList();

        for (int i=0; i < fileList.Count; i++) {

            url = fileList[i].GetUrl();
            Debug.Log(fileList[i].GetUrl());
        }
       
    }
    public IEnumerator LoadImg()
    {
        yield return 0;
        WWW imgLink = new WWW(url);
        yield return imgLink;
        img = imgLink.texture;
    }

    void OnGUI()
    {
        GUILayout.Label(img);
    }

    public void OnException(Exception ex)
    {
        throw new NotImplementedException();
    }
}
