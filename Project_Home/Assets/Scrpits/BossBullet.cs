﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.GetInstance().GameOver();
        }
    }
}
