using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDropDestroyer : MonoBehaviour {

    List<TrashData> trashList = TrashRandomManager.GetInstance().GetTrash();
    TrashData trash;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        trash = trashList.Find(trash => trash.TrashName.Equals(collision.gameObject.name));

        if (trash != null)
        {
            print(trash.TrashSegType.ToUpper());
            if(trash.TrashSegType.ToUpper().Equals(gameObject.name.ToUpper()))
            {
                Destroy(collision.gameObject);
            }
            else
            {
                print("WRONG");
            }
        }
        else
        {
            print("NULL:" + trash);
        }
        
        
    }
}
