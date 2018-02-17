using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.storage;
using UnityEngine.UI;
using System;
using LitJson;

public class DIYScript : MonoBehaviour {

    // Use this for initialization

    TrashData trash;

    private void Start()
    {
        trash = TrashManager.GetInstance().GetTrash();

        Constant cons = new Constant();
        App42API.Initialize(cons.apiKey, cons.secretKey);
        Query query = QueryBuilder.Build("DIYTrashName", "plastic plate", Operator.EQUALS);
        StorageService storageService = App42API.BuildStorageService();
        storageService.FindDocumentsByQuery(cons.dbName, "DIYFile", query, new DIYResponse());
    }

    public void GotoProcedures()
    {                
        SceneManager.LoadScene("DIY_procedure");
    }
   
}

internal class DIYResponse : App42CallBack
{
    public Text procedure;
    public Text prepareText;
    public Text name;

    DIYTrashData[] diy;

    public void OnSuccess(object response)
    {
        Storage storage = (Storage)response;
        IList<Storage.JSONDocument> jsonDocList = storage.GetJsonDocList();
        diy = new DIYTrashData[2];        

        for (int i = 0; i < jsonDocList.Count; i++)
        {
            diy[i] = JsonUtility.FromJson<DIYTrashData>(jsonDocList[i].GetJsonDoc());
            
        }

        Scene scene = SceneManager.GetActiveScene();

        if (scene.name.Equals("DIY"))
        {            
            GameObject.Find("name_diy1").GetComponent<Text>().text = diy[0].DIYCraftName;
            GameObject.Find("name_diy2").GetComponent<Text>().text = diy[1].DIYCraftName;
            IEnumerator callEnum = FindImage();
            while(callEnum.MoveNext())
            {

            }
            
                        
        }
    }

   IEnumerator FindImage()
    {
        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Ocp-Apim-Subscription-Key", "1b4d9849f0e14b3a815c56c5958832be");

        for(int i=0; i < diy.Length; i++)
        {
            string craft = diy[i].DIYCraftName.Replace(' ', '+').ToLower();
            var url = "https://api.cognitive.microsoft.com/bing/v7.0/search?q=" + craft + "&answerCount=1&responseFilter=images&mkt=en-us";
            
            using (WWW www = new WWW(url, null, headers))
            {
                yield return www;

                if (string.IsNullOrEmpty(www.error))
                {
                        JsonData picUrl = JsonMapper.ToObject(www.text);

                        Debug.Log("IN");
                        Texture2D tex = new Texture2D(621, 397, TextureFormat.DXT1, false);
                        using (WWW w = new WWW(picUrl["images"]["value"][i]["thumbnailUrl"].GetString()))
                        {
                            yield return w;
                            w.LoadImageIntoTexture(tex);
                            GameObject.Find("BtnSample" + (i + 1)).GetComponent<Image>().material.mainTexture = tex;
                        }
                 
                }
                else
                {
                    Debug.Log("Error: " + www.error);
                }
            }
        }        
    }

    public void DisplayCraftDetails()
    {
        name = GameObject.Find("name_diy").GetComponent<Text>();
        prepareText = GameObject.Find("Procedure").GetComponent<Text>();
        procedure = GameObject.Find("prepareText").GetComponent<Text>();
    }    

    public void OnException(Exception ex)
    {
        throw new NotImplementedException();
    }   
}
