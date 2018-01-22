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

		/*
		if (Input.touchCount > 0) {
			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved) {
				Vector3 touchedPos = Camera.main.ScreenToWorldPoint (new Vector3 (touch.position.x,touch.position.y,10));
				transform.position = Vector3.Lerp (transform.position,touchedPos,Time.deltaTime);
			}
		}
		*/
	}
}
