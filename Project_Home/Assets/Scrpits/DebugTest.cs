using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void KillBoss() {
        var _target = GameObject.FindWithTag("Boss").transform;

        if (_target!=null) {
            if (_target.GetComponent<BottleMonster>() != null) {
                _target.GetComponent<BottleMonster>()._bloodNum = 0;
            }

            if (_target.GetComponent<MouthEarMonster>() != null) {
                _target.GetComponent<MouthEarMonster>()._bloodNum = 0;
            }

            if (_target.GetComponent<HeartMonster>() != null) {
                // GameManager.GetInstance().ShowdFinishBG();
                //_target.GetComponent<HeartMonster>().StopAllCoroutines();
                _target.GetComponent<HeartMonster>()._bloodNum = 0;
            }
        }
       
    }
}
