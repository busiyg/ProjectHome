using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class MouthEarMonster : MonoBehaviour
{
    public GameObject ShotCtrl;
    public float Speed;
    public float RotateSpeed;
    public Transform MouthMonstImg;
    public float AimTime;
    public float ShootTime;

    public bool IsShot;

    private string _stateName;
    private Transform _target;

    private float angle;

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
            case "Shoot":
                Shoot();
                //if (!IsShot)
                //{
                    
                //}
                //else
                //{

                //}
                break;
            case "Dizzy":
                break;
        }


	    if (Vector3.Dot(Vector3.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x, 0)) > 0)
	    {
	        Debug.Log("左边");
	        Debug.DrawRay(transform.position, transform.right * -100, Color.red);
	        Debug.DrawRay(transform.position, Vector3.up * 100, Color.red);
        }
	    if (Vector3.Dot(Vector3.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x, 0)) < 0)
	    {
	        Debug.Log("右边");
	        Debug.DrawRay(transform.position, transform.right * 100, Color.red);
	        Debug.DrawRay(transform.position, Vector3.up * 100, Color.red);
        }
        
	}

    void CutAim()
    {
        _stateName = "Aim";
    }

    void CutShoot()
    {
        MouthMonstImg.rotation=Quaternion.identity;
        MouthMonstImg.GetComponent<SpriteRenderer>().flipX = false;
        ShotCtrl.SetActive(true);
        //
        _stateName = "Shoot";
        
    }

    private float _aimTime;
    void Aim()
    {
        if (Vector3.Dot(transform.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x,  0)) > 0)
        {
            Debug.Log("左边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Vector3.Dot(transform.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x,  0)) < 0)
        {
            Debug.Log("右边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = true;
        }
    
        Vector2 dir = _target.position - transform.position;
        transform.Translate(dir * Speed * Time.deltaTime);

        if (_aimTime < AimTime)
        {
            _aimTime += Time.deltaTime;
        }
        else
        {
            CutShoot();
            _aimTime = 0;
        }
    }

    private float _shootTime;
    void Shoot()
    {
        float bulletAngle = 0;
        if (Vector3.Dot(Vector3.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x, 0)) > 0)
        {
            Debug.Log("左边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = false;
            angle = Mathf.Rad2Deg * Mathf.Atan((transform.position.y - _target.position.y) / (transform.position.x - _target.position.x));

            if (transform.position.x - _target.position.x < 0)
            {
                angle = angle - 180;
            }

            bulletAngle = Vector3.Angle(-transform.right, Vector3.up);


        }
        if (Vector3.Dot(Vector3.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x, 0)) < 0)
        {
            Debug.Log("右边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = true;

            angle = Mathf.Rad2Deg * Mathf.Atan((transform.position.y - _target.position.y) / (transform.position.x - _target.position.x));

            if (transform.position.x - _target.position.x < 0)
            {
                //angle = angle - 180;
            }

            bulletAngle = Vector3.Angle(transform.right, Vector3.up);

            bulletAngle = 360 - bulletAngle;
        }

        ShotCtrl.GetComponent<UbhShotCtrl>()._ShotList[0]._ShotObj.GetComponent<UbhPaintShot>()._PaintCenterAngle = bulletAngle;


        transform.localEulerAngles = new Vector3(0, 0, angle);

        if (_shootTime < ShootTime)
        {
            _shootTime += Time.deltaTime;
        }
        else
        {
            //ShotCtrl.SetActive(true);
            //CutShoot();
            //_aimTime = 0;
        }
    }
}
