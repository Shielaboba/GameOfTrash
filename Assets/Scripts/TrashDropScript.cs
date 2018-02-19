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

    private void Start()
    {
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
    }

    IEnumerator DeployTrash()
    {
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
