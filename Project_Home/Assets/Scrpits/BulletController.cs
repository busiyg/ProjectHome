using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {
    public float Speed;
    public Vector3 Dir;
	// Use this for initialization
	void Start () {
        Destroy(gameObject,3);
	}

    public void InitBullet(Vector3 dir) {
        Dir = dir;

        gameObject.transform.position =
            new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, 0);
    }
	
	// Update is called once per frame
	void Update () {
        if (Dir != null) {
            gameObject.transform.Translate(Dir * Speed);
        }
	}
}
