using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MilitaryRid : MonoBehaviour {

	private MilitarySceneController sceneController;
    private Animator animator;
	public float speed = 0.1f;
	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		sceneController = GameObject.Find ("/GameController").GetComponent<MilitarySceneController> ();
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {

		var verticalAxis = Input.GetAxis ("Vertical");
		var horizontalAxis = Input.GetAxis ("Horizontal");

		var offset = new Vector3 (horizontalAxis * speed, verticalAxis * speed);

		animator.SetBool("isWalking", offset != Vector3.zero);
		GetComponent<SpriteRenderer> ().flipX = offset.x > 0;

		transform.position += offset;

		if (Input.GetMouseButtonDown (0)) {
			Fire ();
		}
	}

	private void Fire()
	{
		var bullet = (GameObject)Instantiate(
			bulletPrefab,
			transform.position,
			Quaternion.identity);

		// Add velocity to the bullet
		bullet.GetComponent<Rigidbody2D>().velocity = new Vector2(0.0f, 5.0f);

		// Destroy the bullet after 2 seconds
		Destroy(bullet, 2.0f); 
	}
		
	void OnTriggerEnter2D(Collider2D other) {

		if (other.gameObject.name == "BottonWallGismo") {
			sceneController.OnBottomWallReached ();
		}

        if (other.gameObject.tag == "EnemyBullet")
        {
            animator.SetTrigger("hit");
            Destroy(other.gameObject);
        }
	}
}
