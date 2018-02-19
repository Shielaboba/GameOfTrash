using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using com.shephertz.app42.paas.sdk.csharp;
using com.shephertz.app42.paas.sdk.csharp.upload;
using System;

public class TrashDropScript : MonoBehaviour
{

    public GameObject[] obj;
    List<string> urls;
    //public string url = "http://cdn.shephertz.com/repository/files/6d3315644df7ab693ec31e305146d091e5bd99ef48d5b059d9564f47506b7cd5/5b40e645c8d6827394d41ddaf53aeb3e452b29b5/tire.png";

    private void Start()
    {
        //print("STARt");
        SelectLevelGame();
        StartCoroutine("DeployTrash");
    }


    public void SelectLevelGame()
    {
        int level = LevelManager.GetInstance().GetSelectLevel();

        if (!gameObject.name.Equals("TrashSegLevel" + level))
        {                
            gameObject.SetActive(false);
        }


        //if (urls != null)
        //    StartCoroutine("DeployTrash");
        //else
        //{
        //    print("NULL");
        //}
    }

    IEnumerator DeployTrash()
    {
        print("IN");
        urls = TrashUrlManager.GetInstance().GetURL();
        for (int i = 0; i < obj.Length; i++)
        {
            WWW www = new WWW(urls[i]);
            yield return www;

            if (www.isDone) obj[i].SetActive(true);
            obj[i].GetComponent<Image>().sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), new Vector2(0, 0));
            Instantiate(obj[i]);
        }
    }
}
