using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class HeartMonster : MonoBehaviour
{
    public GameObject Left;
    public GameObject Right;
    public GameObject Center;
    public GameObject Hit;

    public GameObject LeftBorder;
    public GameObject RightBorder;

    public GameObject LeftCollider;
    public GameObject RightCollider;

    public float Speed;
    public float ShakeTime;
    public float XShakeWidth;
    public float YShakeWidth;

    public List<GameObject> WaterList=new List<GameObject>();

    private string _stateName;
    private Transform _target;
    private int Hp = 3;

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

    void OpenCollider()
    {
        LeftCollider.SetActive(true);
        RightCollider.SetActive(true);

        Left.SetActive(false);
        Right.SetActive(false);

        LeftBorder.GetComponent<BoxCollider2D>().enabled = false;
        RightBorder.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void CloseCollider()
    {
        LeftCollider.SetActive(false);
        RightCollider.SetActive(false);

        Left.SetActive(true);
        Right.SetActive(true);

        //LeftBorder.GetComponent<BoxCollider2D>().enabled = false;
        //RightBorder.GetComponent<BoxCollider2D>().enabled = false;
    }

    IEnumerator CutClipCoroutine()
    {
        _stateName = "Clip";

        Left.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));
        Right.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));

        Left.GetComponent<EdgeCollider2D>().enabled = false;
        Right.GetComponent<EdgeCollider2D>().enabled = false;

        LeftBorder.GetComponent<BoxCollider2D>().enabled = true;
        RightBorder.GetComponent<BoxCollider2D>().enabled = true;

        yield return new WaitForSeconds(ShakeTime + 0.1f);

        LeftCollider.transform.localPosition = new Vector3(-0.5f, 0, 0);
        RightCollider.transform.localPosition = new Vector3(0.5f, 0, 0);
        //Hit.GetComponent<Collider2D>().enabled = true;
        Left.transform.DOLocalMoveX(-0.5f, 0.2f);
        //Right.transform.DOLocalMoveX(0.5f, 0.2f);

        Tweener dolocal= Right.transform.DOLocalMoveX(0.5f, 0.2f);

        dolocal.onComplete += OpenCollider;

        if (WaterList.Count > 3) {
            Hp -= 1;
            LeftCollider.GetComponent<SpriteRenderer>().DOColor(Color.black, 0.5f).OnComplete(() => {
                LeftCollider.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.5f);
            }); ;
            RightCollider.GetComponent<SpriteRenderer>().DOColor(Color.black, 0.5f).OnComplete(() => {
                RightCollider.GetComponent<SpriteRenderer>().DOColor(Color.white, 0.5f);
            }); ;
            foreach (var obj in WaterList) {
                Destroy(obj.gameObject);
            }
            WaterList.Clear();
        } else {
            foreach (var obj in WaterList) {
                Destroy(obj.gameObject);
            }
            WaterList.Clear();
        }

        if (Hp<=0) {
            GameManager.GetInstance().ShowdFinishBG();
            StopAllCoroutines();
        }

        yield return  new WaitForSeconds(1.0f);
        //Hit.GetComponent<Collider2D>().enabled = false;
        // Left.GetComponent<EdgeCollider2D>().enabled = false;
        // Right.GetComponent<EdgeCollider2D>().enabled = false;
        LeftCollider.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));
        RightCollider.transform.DOShakePosition(ShakeTime, new Vector2(XShakeWidth, YShakeWidth));
        yield return new WaitForSeconds(ShakeTime);


        Left.transform.localPosition = new Vector3(-1.5f, 0, 0);
        Right.transform.localPosition = new Vector3(1.5f, 0, 0);
        LeftCollider.transform.DOLocalMoveX(-1.5f, 0.2f);
        Tweener doooo= RightCollider.transform.DOLocalMoveX(1.5f, 0.2f);
        doooo.onComplete += CloseCollider;

        yield return new WaitForSeconds(0.5f);

        Center.GetComponent<HeartCenter>().IsClip = false;
        CutAim();
    }

    void Aim()
    {
        if (_target!=null) {
            Vector2 dir = _target.position - transform.position;
            transform.Translate(dir * Speed * Time.deltaTime);
        }   
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        Debug.Log(collider.name);
    }
}
