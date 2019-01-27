using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorController : MonoBehaviour {
    public Animator Ani;
    public GameObject Button;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D collider) {

        if (collider.tag.Equals("Player")) {
            SceneManager.LoadScene("Game");
        }
    }

    public void UIMove() {
        Ani.Play("Move");
        Button.SetActive(false);
    }

}
