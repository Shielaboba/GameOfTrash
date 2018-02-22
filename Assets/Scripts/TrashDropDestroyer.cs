using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDropDestroyer : MonoBehaviour {

    LifeManager life_manager;
    List<TrashData> trashList = TrashRandomManager.GetInstance().GetTrash();
    TrashData trash;

    private void Start()
    {
        life_manager = FindObjectOfType<LifeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trash = trashList.Find(trash => trash.TrashName.Equals(collision.gameObject.name));

        if (trash != null)
        {
            print(trash.TrashSegType.ToUpper());
            if(trash.TrashSegType.ToUpper().Equals(gameObject.name.ToUpper()))
            {
                ScoreScript.AddPoints(10);
                Destroy(collision.gameObject);
             
            }
            else
            {
                life_manager.TakeLife();
                print("WRONG");
            }
        }
        else
        {
            
            print("NULL:" + trash);
            
        }
        
        
    }
}
