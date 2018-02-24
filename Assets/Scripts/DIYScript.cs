using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.UI;
using System;
using LitJson;

public class DIYScript : MonoBehaviour
{
    // Use this for initialization
    TrashData trash;

    private void Start()
    {
        trash = TrashManager.GetInstance().GetTrash();
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);
        Query query = QueryBuilder.Build("DIYTrashName", trash.TrashName, Operator.EQUALS);
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentsByQuery(cons.dbName, "DIYFile", query, new DIYResponse());
    }

}

internal class DIYResponse : App42CallBack
{
    public Text procedure;
    public Text prepareText;
    public Text name;
    Text btnText;
    List<DIYTrashData> diy;

    public void OnSuccess(object response)
    {
        Storage storage = (Storage)response;
        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
        diy = new List<DIYTrashData>();

        for (int i = 0; i < jsonDocList.Count; i++)
        {
            diy.Add(JsonUtility.FromJson<DIYTrashData>(jsonDocList[i].GetJsonDoc()));

        }
        TrashRandomManager.GetInstance().SetDIYTrash(diy);
    }



    public void DisplayCraftDetails()
    {
        name = GameObject.Find("name_diy").GetComponent<Text>();
        prepareText = GameObject.Find("Procedure").GetComponent<Text>();
        procedure = GameObject.Find("prepareText").GetComponent<Text>();
    }

    public void OnException(Exception ex)
    {
        btnText = GameObject.Find("BtnDiy").GetComponentInChildren<Text>();
        btnText.text = "Go back to Trash Hunt";
    }
}
