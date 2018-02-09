using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.UI;
using System;

public class DIYScript : MonoBehaviour {

    // Use this for initialization

    TrashData trash;

    [System.Serializable]
    public class DIYTrashData
    {
        public String DIYTrashName;
        public String DIYTrashCraftName;
        public String[] DIYProcedure;
    }

    private void Start()
    {
        trash = TrashManager.GetInstance().GetTrash();
    }

    public void GotoProcedures()
    {
        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);
        Query query = QueryBuilder.Build("DIYName", trash.TrashCraftMade, Operator.EQUALS);
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentsByQuery(cons.dbName, "DIYTrashFile", query, new TriviaResponse());

        SceneManager.LoadScene("DIY_procedure");
    }
}

internal class DIYResponse : App42CallBack
{   
    public void OnSuccess(object response)
    {
        Storage storage = (Storage)response;
        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();

    }

    public void OnException(Exception ex)
    {
        throw new NotImplementedException();
    }   
}
