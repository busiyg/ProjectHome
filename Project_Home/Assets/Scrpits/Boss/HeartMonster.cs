using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeartMonster : MonoBehaviour
{
    public GameObject Left;
    public GameObject Right;
    public GameObject Center;
    public float Speed;
    public float ShakeTime;
    public float XShakeWidth;
    public float YShakeWidth;

    private string _stateName;
    private Transform _target;

    // Use this for initialization
    void Start ()
	{
	    _target = GameObject.FindWithTag("Player").transform;

	    CutAim();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    switch (_stateName)
	    {
            case "Aim":
                Aim();
                break;
            case "Clip":
                break;
            case "Dizzy":
                break;
	    }
	}

    void CutAim()
    {
        _stateName = "Aim";
    }

    public void CutClip()
    {
        StartCoroutine(CutClipCoroutine());
    }

    IEnumerator CutClipCoroutine()
    {
        _stateName = "Clip";

        Left.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));
        Right.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));

        yield return new WaitForSeconds(1.1f);

        Left.GetComponent<EdgeCollider2D>().enabled = true;
        Right.GetComponent<EdgeCollider2D>().enabled = true;
        Left.transform.DOLocalMoveX(-0.5f, 0.2f);
        Right.transform.DOLocalMoveX(0.7f, 0.2f);

        yield return  new WaitForSeconds(1.0f);
        Left.GetComponent<EdgeCollider2D>().enabled = false;
        Right.GetComponent<EdgeCollider2D>().enabled = false;
        Left.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));
        Right.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));
        yield return new WaitForSeconds(ShakeTime);

        
        Left.transform.DOLocalMoveX(-1.5f, 0.2f);
        Right.transform.DOLocalMoveX(1.5f, 0.2f);

        yield return new WaitForSeconds(0.5f);

        Center.GetComponent<HeartCenter>().IsClip = false;
        CutAim();
    }

    void Aim()
    {
        Vector2 dir = _target.position - transform.position;
        transform.Translate(dir * Speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
    }
}
