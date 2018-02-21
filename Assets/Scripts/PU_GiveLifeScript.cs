using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PU_GiveLifeScript : MonoBehaviour {
    public Text addedLife;
    LifeManager life_manager;
  // int holderAddedLife;
    int count;
  
	// Use this for initialization
	void Start () {
       addedLife = GameObject.Find("countLifeAdded").GetComponent<Text>();
        life_manager = FindObjectOfType<LifeManager>();
        // holderAddedLife = int.Parse(addedLife.text.ToString());
        count = 2;
      //  
    }
	
	// Update is called once per frame
	void Update () {

        addedLife.text = "" + count;
    }

    public void OnClick()
    {
       
        if (count!=0)
        {
            life_manager.GiveLife();
            count--;
        }
       
        if (count < 0)
        {
            count = 0;
            Debug.Log("No more power up");
            addedLife.text = "" + 0;
        }
        

    }
}
