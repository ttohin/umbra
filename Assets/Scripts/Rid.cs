using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rid : MonoBehaviour {

    private Animator animator;

    public float moveTime = 5.0f; //Time it will take object to move, in seconds.
    private Rigidbody2D rb2D; //The Rigidbody2D component attached to this object.
    private CircleCollider2D bottomCollider;
    private float inverseMoveTime; //Used to make movement more efficient.
    private Vector3 destinatinPosition = Vector3.zero;
    private Vector3 destinatinPositionOffset = new Vector3(0, 0.9f, 0);

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator> ();
        rb2D = GetComponent<Rigidbody2D> ();
        bottomCollider = GetComponent<CircleCollider2D>();
        inverseMoveTime = 1f / moveTime;
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetMouseButtonDown (0)) {
            var mousePosition = Camera.main.ScreenToWorldPoint (Input.mousePosition);
            mousePosition.z = 0.0f;
            destinatinPosition = mousePosition;
            StartCoroutine (SmoothMovement ());
        }
    }

    //Co-routine for moving units from one space to next, takes a parameter end to specify where to move to.
    protected IEnumerator SmoothMovement () {
        animator.SetBool ("isWalking", true);

        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        //Square magnitude is used instead of magnitude because it's computationally cheaper.
        var rb2Dposition = new Vector3 (rb2D.position.x, rb2D.position.y);
        float sqrRemainingDistance = (rb2Dposition - GetTargetPosition()).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance >= 0.005f && destinatinPosition != Vector3.zero) {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards (rb2D.position, GetTargetPosition(), Time.deltaTime * 5.0f);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            rb2D.MovePosition (newPostion);
            rb2Dposition = new Vector3 (rb2D.position.x, rb2D.position.y);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (rb2Dposition - GetTargetPosition()).sqrMagnitude;

            // Flib sprite if we move to right
            GetComponent<SpriteRenderer> ().flipX = GetTargetPosition().x > rb2D.position.x;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        animator.SetBool ("isWalking", false);
    }

    private Vector3 GetTargetPosition()
    {
        return destinatinPosition + destinatinPositionOffset;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        destinatinPosition = Vector3.zero;
    }

}