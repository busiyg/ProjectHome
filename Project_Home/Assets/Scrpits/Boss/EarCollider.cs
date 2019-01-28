using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarCollider : MonoBehaviour
{
    public MouthEarMonster mouthMonster;

	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Player"))
        {
            GameManager.GetInstance().GameOver();
        }

        //Debug.Log(collider);
        if (collider.tag.Equals("love"))
        {
            collider.gameObject.SetActive(false);
            Debug.Log("打中耳朵");
            mouthMonster.Hurt();
        }
    }
}
