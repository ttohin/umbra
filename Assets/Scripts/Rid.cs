using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rid : MonoBehaviour {

	private Animator animator;

	public float moveTime = 5.0f;			//Time it will take object to move, in seconds.
	public LayerMask blockingLayer;			//Layer on which collision will be checked.


	private CircleCollider2D collider; 		//The BoxCollider2D component attached to this object.
	private Rigidbody2D rb2D;				//The Rigidbody2D component attached to this object.
	private float inverseMoveTime;			//Used to make movement more efficient.
	bool isMoving = false;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator>();

		//Get a component reference to this object's BoxCollider2D
		collider = GetComponent <CircleCollider2D> ();

		//Get a component reference to this object's Rigidbody2D
		rb2D = GetComponent <Rigidbody2D> ();

		//By storing the reciprocal of the move time we can use it by multiplying instead of dividing, this is more efficient.
		inverseMoveTime = 1f / moveTime;
	}
	
	// Update is called once per frame
	void Update () {


		if (Input.GetMouseButtonDown (0)) {
			

			var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
			mousePosition.z = 0.0f;


			GetComponent<SpriteRenderer> ().flipX = mousePosition.x > rb2D.position.x;

			isMoving = true;

			StartCoroutine (SmoothMovement (mousePosition));
		}
	}


	//Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
	protected IEnumerator SmoothMovement (Vector3 end)
	{
		animator.SetBool ("isWalking", true);

		//Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
		//Square magnitude is used instead of magnitude because it's computationally cheaper.
		var rb2Dposition = new Vector3(rb2D.position.x, rb2D.position.y);

		float sqrRemainingDistance = (rb2Dposition - end).sqrMagnitude;

		//While that distance is greater than a very small amount (Epsilon, almost zero):
		while(sqrRemainingDistance >= 0.005f && isMoving)
		{
			//Find a new position proportionally closer to the end, based on the moveTime
			Vector3 newPostion = Vector3.MoveTowards(rb2D.position, end, Time.deltaTime * 5.0f);

			//Call MovePosition on attached Rigidbody2D and move it to the calculated position.
			rb2D.MovePosition (newPostion);

			rb2Dposition = new Vector3(rb2D.position.x, rb2D.position.y);

			//Recalculate the remaining distance after moving.
			sqrRemainingDistance = (rb2Dposition - end).sqrMagnitude;


			//Return and loop until sqrRemainingDistance is close enough to zero to end the function
			yield return null;
		}

		isMoving = false;
		animator.SetBool ("isWalking", false);
	}
		
}
