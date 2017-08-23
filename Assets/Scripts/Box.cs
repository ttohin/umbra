using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour {

	private int health = 2;
	public Sprite brokenSprite;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D other) {

		if (health == 0)
			return;

		if (other.gameObject.tag == "Bullet") {

			Destroy (other.gameObject);

			health -= 1;
			if (health == 0) {
				GetComponent<SpriteRenderer> ().sprite = brokenSprite;
				Destroy (GetComponent<BoxCollider2D> ());
			}
		}
	}
}
