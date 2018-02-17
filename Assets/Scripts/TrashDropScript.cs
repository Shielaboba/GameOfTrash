using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashDropScript : MonoBehaviour {

	//public float speed = 10F;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved) {
			Vector2 touchDeltaPosition = Input.GetTouch(0).deltaPosition;            
			transform.Translate(touchDeltaPosition.x, touchDeltaPosition.y, 0);
		}
	}
}
