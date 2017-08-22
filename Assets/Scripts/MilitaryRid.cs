using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryRid : MonoBehaviour {

	private MilitarySceneController sceneController;
	public float speed = 0.1f;

	// Use this for initialization
	void Start () {
		sceneController = GameObject.Find ("/GameController").GetComponent<MilitarySceneController> ();
	}
	
	// Update is called once per frame
	void Update () {

		var verticalAxis = Input.GetAxis ("Vertical");
		var horizontalAxis = Input.GetAxis ("Horizontal");

		transform.position += new Vector3 (horizontalAxis * speed, verticalAxis * speed);
	}
		
	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.name == "BottonWallGismo") {
			sceneController.OnBottomWallReached ();
		}
	}
}
