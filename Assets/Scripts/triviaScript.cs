using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.SceneManagement;

public class TriviaScript : MonoBehaviour {

    // Use this for initialization
    string collectionName = "TrashFile";
    string keyName = "TrashName";
    public GameObject window;
    TrashData trash;

    private void Start()
    {
        trash = TrashManager.GetInstance().GetTrash();    
    }

    public void Show()
    {
        if (gameObject.GetComponentInChildren<Text>().text.ToUpper().Equals(trash.TrashSegType.ToUpper()))
        {
            SceneManager.LoadScene("trivia_menu");
            Constant cons = new Constant();
            App42API.Initialize(cons.apiKey, cons.secretKey);
            Query query = QueryBuilder.Build(keyName, trash.TrashName, Operator.EQUALS);
            StorageService storageService = App42API.BuildStorageService();
            storageService.FindDocumentsByQuery(cons.dbName, collectionName, query, new TriviaResponse());

            window.SetActive(true);
        }
        else
        {
            print("GO: " + gameObject.GetComponentInChildren<Text>().text.ToUpper() + " trash: " + trash.TrashSegType.ToUpper());
            GameObject.Find("Title").GetComponent<Text>().text = "Incorrect Type";
        }
           
    }

    public void Hide()
    {
        window.SetActive(false);
    }

    public void OnClick()
    {
        SceneManager.LoadScene("DIY");        
    }
}
