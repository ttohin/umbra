using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableItem : MonoBehaviour {

    private bool hasContactWithRid = false;
    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update () {

    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Rid") {
            Debug.Log ("Bottle Contact with Rid");
            hasContactWithRid = true;
            Take ();
        }
    }

    void OnMouseDown () {
        if (hasContactWithRid) {
            Take ();
        }
    }

    void Take () {
        Debug.Log("Bottle's been taken");
        Destroy (this.gameObject);
    }
}