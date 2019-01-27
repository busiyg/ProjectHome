using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartLeft : MonoBehaviour
{

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            //GameManager.GetInstance().GameOver();
            Debug.Log("左心打中主角");
        }
    }
}
