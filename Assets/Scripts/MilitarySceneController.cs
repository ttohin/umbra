using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MilitarySceneController : MonoBehaviour {

    private GameObject bg0;
    private GameObject bg1;

    public float scrollSpeed = 0.5f;

    // Use this for initialization
    void Start () {
        bg0 = GameObject.Find ("Background0");
        bg1 = GameObject.Find ("Background1");
    }

    // Update is called once per frame
    void Update () {

        Vector3 targetPosition = new Vector3 (transform.position.x, transform.position.y - scrollSpeed);
        transform.position = Vector3.Lerp (transform.position, targetPosition, Time.deltaTime);
    }

    public void OnBottomWallReached () {
        SceneManager.LoadScene ("Scenes/Main");
        // transform.position = Vector3.zero;
        // var rid = GameObject.Find("Rid");
        // rid.transform.position = Vector3.zero;
    }
}