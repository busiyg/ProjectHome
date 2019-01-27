using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAni : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Ani_Finish(int K) {
        GameManager.GetInstance().ChangeLevel(K);
    }
}
