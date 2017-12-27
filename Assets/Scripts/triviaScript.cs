using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.user;
using com.shephertz.app42.paas.sdk.csharp.storage;

public class triviaScript : MonoBehaviour {

    // Use this for initialization
    string collectionName = "TriviaFile";
    string keyName = "TrashName";
    public GameObject window;
    public Text messageField;
    string trashHolderValue = "";
    Text trashTxt;


  

    public void Show(string message)
    {
       
        trashTxt = GameObject.Find("btnText").GetComponent<Text>();
        

       trashHolderValue = trashTxt.text;
      
            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            Query query = QueryBuilder.Build(keyName, trashHolderValue, Operator.EQUALS);
            StorageService storageService = App42API.BuildStorageService();
            storageService.FindDocumentsByQuery(cons.dbName, collectionName, query, new TriviaResponse());
            

          
      

        
            window.SetActive(true);
        
    }

    public void Hide()
    {

        window.SetActive(false);
    }
}
