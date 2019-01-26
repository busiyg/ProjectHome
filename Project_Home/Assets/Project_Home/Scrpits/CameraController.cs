using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public Transform Target;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        CameraFollow();

    }

    public void CameraFollow() {
        var TarPos = new Vector3(Target.position.x, Target.position.y+2, -10);
        transform.position = Vector3.Lerp(transform.position, TarPos, 0.1f);
    }
}
