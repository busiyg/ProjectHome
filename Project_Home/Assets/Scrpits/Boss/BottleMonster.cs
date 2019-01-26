using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using DG.Tweening;
using UnityEngine;

public class BottleMonster : MonoBehaviour
{
    public float Rate;

    private float _speed;
    public float ASpeed;
    public SpriteRenderer BottleImg;
    public float HurtTime;


    private Transform _target;
    public Animator Anim;

    private float angle;

    private string stateName;

    private int _bloodNum = 3;

    // Use this for initialization
    void Start ()
    {
        _target = GameObject.FindWithTag("Player").transform;

        CutAim();
    }
    
    void Update ()
	{
	    if (Input.GetKeyDown(KeyCode.E))
	    {
	        CutState("Shoot");
        }

	    if (Input.GetKeyDown(KeyCode.R))
	    {
	        if (stateName == "Dizzy" && !_isHurt)
	        {
	            StartCoroutine(HurtCoroutine());
            }
        }

	    if (_bloodNum == 0)
	    {
            gameObject.SetActive(false);
            GameManager.GetInstance().ChangeLevelT(2);

        }

	    switch (stateName)
	    {
	        case "Aim":
	            Aim();
                break;
	        case "Shoot":
	            Shoot();
                break;
            case "Dizzy":
                Dizzy();
                break;
	    }
    }

    void Aim()
    {
        angle = Mathf.Rad2Deg * Mathf.Atan((transform.position.y - _target.position.y) / (transform.position.x - _target.position.x));

        if (transform.position.x - _target.position.x < 0)
            angle = angle - 90;

        else
            angle = angle + 90;

        transform.localEulerAngles = new Vector3(0, 0, angle);

        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && Anim.GetCurrentAnimatorStateInfo(0).IsName("BottlePrepare"))
        {
            CutShot();
        }
    }

    void Shoot()
    {
        _speed += ASpeed;
        transform.position += transform.up * _speed * Time.deltaTime;
    }

    private float _dizzyTime = 0;
    public float DizzyIntervalTime;
    private bool _isHurt;
    void Dizzy()
    {
        //播放眩晕特效

        if (_dizzyTime < DizzyIntervalTime)
        {
            _dizzyTime += Time.deltaTime;
        }
        else
        {
            Fire();
        }
    }

    void Fire()
    {
        Anim.SetTrigger("Rotate");
        if (Anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 1.0f && Anim.GetCurrentAnimatorStateInfo(0).IsName("BottleRotate"))
        {
            _dizzyTime = 0;
            Anim.SetBool("Fire", false);
            Anim.ResetTrigger("Idle");
            Anim.ResetTrigger("Rotate");
            _isHurt=false;
            CutAim();
        }
    }

    IEnumerator HurtCoroutine()
    {
        _isHurt = true;
        _bloodNum--;
        BottleImg.DOColor(Color.red, HurtTime);
        yield return new WaitForSeconds(HurtTime);
        BottleImg.DOColor(Color.white, HurtTime);

        _dizzyTime = DizzyIntervalTime + 1;
    }

    void CutAim()
    {
        _speed = 0;
        stateName = "Aim";
    }

    void CutShot()
    {
        Anim.SetBool("Fire", true);
        stateName = "Shoot";
    }

    void CutDizzy()
    {
        _speed = 0;
        Anim.SetTrigger("Idle");
        stateName = "Dizzy";
    }
    
    void CutState(string name)
    {
        switch (name)
        {
            case "Aim":
                
                break;
            case "Shoot":
                
                break;
            case "Dizzy":
                _speed = 0;
                Anim.SetTrigger("Idle");
                stateName = name;
                break;
            case "Normal":
                Anim.SetBool("Fire", false);
                _speed = 0;
                stateName = "Aim";
                break;
            case "HurtCoroutine":
                StartCoroutine(HurtCoroutine());
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.name == "BackGround")
        {
            if (_speed >= 22)
            {
                if (stateName == "Dizzy" || stateName == "Normal")
                    return;

                Debug.Log("眩晕");
                CutDizzy();
            }
            else
            {
                if (stateName == "Dizzy" || stateName == "Normal")
                    return;

                Debug.Log("不眩晕");
                Anim.SetBool("Fire", false);
                _speed = 0;
                CutAim();
            }
        }

        if (collider.tag == "shitou")
        {

            if (stateName == "Dizzy" && !_isHurt)
              
                CutState("HurtCoroutine");
        }

        if (collider.tag == "Player") {

            GameManager.GetInstance().GameOver();
        }
    }
}
