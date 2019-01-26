using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    public float SpeedScale;
    public float JumpSpeed;
    public bool IsJump;
    public Rigidbody2D PlayerRigidbody;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        HorizontialMove();
        VerticalMove();
    }

    public void HorizontialMove() {
        transform.Translate(Input.GetAxis("Horizontal") * SpeedScale,0,0);
    }

    public void VerticalMove() {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("space");
            PlayerRigidbody.AddForce(Vector2.up*JumpSpeed);
        }

    }
}
