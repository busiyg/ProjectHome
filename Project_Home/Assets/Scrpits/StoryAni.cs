using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryAni : MonoBehaviour {
    public Animator storyAnimator;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}




    public void Finish0() {
        GameManager.GetInstance().ChangeLevel(1);
    }

    public void Finish1() {
        GameManager.GetInstance().ChangeLevel(2);
    }

    public void Finish2() {
        GameManager.GetInstance().ChangeLevel(3);
    }

    public void Finish3() {
        //GameManager.GetInstance().ChangeLevel(K);
    }
}
