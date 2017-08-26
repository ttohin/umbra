using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour {

    public bool Opened = false;
    public Sprite OpenedDoorSprite;
    public Sprite ClosedDoorSprite;
    private bool hasContactWithRid = false;
    public string nextSceneName;

    // Use this for initialization
    void Start () {
        GetComponent<SpriteRenderer>().sprite = Opened ? OpenedDoorSprite : ClosedDoorSprite;
    }

    public void Open()
    {
        if (!Opened)
        {
            GetComponent<Animator>().SetTrigger("opening");
        }
    }
    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.name == "Rid") {
            hasContactWithRid = true;
            Open();
        }
    }


    // Will be called after door opening animaiton
    void OnDoorOpened()
    {
        Opened = true;
    }

    void OnMouseDown()
    {
        if(hasContactWithRid && Opened)
        {
            var currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(nextSceneName);
        }
    }
}