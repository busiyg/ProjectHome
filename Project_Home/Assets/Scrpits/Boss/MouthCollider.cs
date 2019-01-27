using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthCollider : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        //Debug.Log(collider);
        if (collider.tag.Equals("Player"))
        {
            Debug.Log("击中主角");
        }
        if (collider.tag.Equals("love"))
        {
            collider.gameObject.SetActive(false);
            Debug.Log("打中嘴");
        }
    }
}
