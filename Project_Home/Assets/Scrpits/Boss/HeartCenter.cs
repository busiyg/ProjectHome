using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCenter : MonoBehaviour
{
    public HeartMonster HeartMonster;
    public float CenterClipTime;

    public bool IsClip;

    private float _inCenterTime;


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
            if (!IsClip)
            {
                if (_inCenterTime < CenterClipTime)
                {
                    _inCenterTime += Time.deltaTime;
                }
                else
                {
                    HeartMonster.CutClip();
                    _inCenterTime = 0;
                    IsClip = true;
                }
            }
            //Debug.Log("Player");
           
        }
        
    }

    void OnTriggerExit2D(Collider2D collider)
    {
        _inCenterTime = 0;
    }
}
