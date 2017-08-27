using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Range (0.01f, 5.0f)]
    public float AttackDelay = 1.0f;
    public GameObject bulletPrefab;
    public int LifeTime = 100;
    public GameObject Rid;

    // Use this for initialization
    void Start () {
        Rid = GameObject.Find("GameController/Rid");
        StartCoroutine (AttackRid ());
    }

    IEnumerator AttackRid () {
        while (LifeTime != 0) {
            yield return new WaitForSeconds (AttackDelay);
            Fire ();
            LifeTime -= 1;
        }
    }

    private void Fire () {
        var bullet = (GameObject) Instantiate (
            bulletPrefab,
            transform.position,
            Quaternion.identity);
        bullet.gameObject.tag = "EnemyBullet";

        Vector3 direction = Rid.transform.position - this.transform.position;
        // Add velocity to the bullet
        bullet.GetComponent<Rigidbody2D> ().velocity = direction.normalized * 5;

        // Destroy the bullet after 2 seconds
        Destroy (bullet, 2.0f);
    }

    // Update is called once per frame
    void Update () {

    }

}