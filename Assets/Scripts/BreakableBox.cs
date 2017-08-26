using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBox : MonoBehaviour {

    private int health = 3;
    private bool hasContactWithRid = false;
    public Sprite breakedSprite;
    public GameObject bottlePrefab;

    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame

    void FixedUpdate () {
        var contacts = new Collider2D[] { };
        GetComponent<BoxCollider2D> ().GetContacts (contacts);
    }

    void OnCollisionEnter2D (Collision2D other) {
        if (other.gameObject.name == "Rid") {
            Debug.Log ("Contact with Rid");
            hasContactWithRid = true;
            Hit ();
        }
    }

    void OnMouseDown () {
        if (hasContactWithRid) {
            Hit ();
        }
    }

    void Hit () {
        GetComponent<Animator> ().SetTrigger ("hit");
    }

    void OnHit()
    {
        health -= 1;
        if (health <= 0) {
            Destroy (GetComponent<BoxCollider2D> ());
            GetComponent<SpriteRenderer> ().sprite = breakedSprite;
            var bottle1 = (GameObject) Instantiate (
                bottlePrefab,
                transform.position,
                Quaternion.identity);
            var bottle2 = (GameObject) Instantiate (
                bottlePrefab,
                transform.position + new Vector3(0.5f, 0),
                Quaternion.identity);
        }
    }
}