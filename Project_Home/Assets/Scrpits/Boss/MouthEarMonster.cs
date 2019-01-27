using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using DG.Tweening;
using UnityEngine;

public class MouthEarMonster : MonoBehaviour
{
    public GameObject ShotCtrl;
    public List<GameObject> ShotCtrlList = new List<GameObject>();
    public float Speed;
    public float RotateSpeed;
    public Transform MouthMonstImg;
    public float AimTime;
    public float DizzyTime;

    public GameObject Ear;

    public SpriteRenderer MouthImg;
    public float HurtTime;

    public GameObject FlipFalseCollider;
    public GameObject FlipTrueCollier;

    private string _stateName;
    private Transform _target;
    public Animator Anim { get; set; }
    public bool Dead = false;

    public bool c;

    private float angle;

    private int _index;

    public  int _bloodNum = 3;

    // Use this for initialization
    void Start ()
	{
	    _target = GameObject.FindWithTag("Player").transform;
	    Anim = MouthMonstImg.GetComponent<Animator>();


        CutAim();
	}
	
	// Update is called once per frame
	void Update ()
	{
        if (Dead) {
            return;
        }
	    if (_target == null)
	    {
	        return;
	    }

	    switch (_stateName)
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

	    if (_bloodNum == 0)
	    {
            print("!!!!!!!!!!!!!!!!!!!");
            Dead = true;
          
            Anim.Play("Death");
            Invoke("Die",3);
        //    gameObject.SetActive(false);
	    }

	    if (Vector3.Dot(Vector3.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x, 0)) > 0)
	    {
	      //  Debug.Log("左边");
	        Debug.DrawRay(transform.position, transform.right * -100, Color.red);
	        Debug.DrawRay(transform.position, Vector3.up * 100, Color.red);
        }
	    if (Vector3.Dot(Vector3.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x, 0)) < 0)
	    {
	       // Debug.Log("右边");
	        Debug.DrawRay(transform.position, transform.right * 100, Color.red);
	        Debug.DrawRay(transform.position, Vector3.up * 100, Color.red);
        }
        
	}

    public void Die() {
        GameManager.GetInstance().FinishLevel();
        Destroy(gameObject);
    }

    void CutAim()
    {
        //Ear.gameObject.SetActive(false);
        Ear.GetComponent<EdgeCollider2D>().enabled = false;

        Anim.SetTrigger("Aim");
        Anim.ResetTrigger("Idle");
        Anim.ResetTrigger("EnterFire");
        Anim.ResetTrigger("Weak");
        //Quaternion q= Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0));
        //transform.localRotation = q;
        // MouthMonstImg.GetComponent<SpriteRenderer>().flipX = false;
        _stateName = "Aim";
    }

    void CutShoot()
    {
        Anim.ResetTrigger("Aim");
        MouthMonstImg.rotation=Quaternion.identity;
        MouthMonstImg.GetComponent<SpriteRenderer>().flipX = false;
        ShotCtrlList[_index].SetActive(true);
        Anim.SetTrigger("Idle");
        _stateName = "Shoot";

        StartCoroutine(ShootCoroutine());
    }

    void CutDizzy()
    {
        Anim.SetTrigger("Weak");
        Quaternion q = Quaternion.Euler(new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, 0));
        transform.localRotation = q;

        Ear.GetComponent<EdgeCollider2D>().enabled = true;
        //Ear.gameObject.SetActive(true);

        _isHurt = true;

        _stateName = "Dizzy";
    }

    IEnumerator ShootCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        Anim.SetTrigger("EnterFire");

        //_stateName = "Shoot";
    }

    private float _aimTime;
    void Aim()
    {
        if (Vector3.Dot(transform.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x,  0)) > 0)
        {
         //   Debug.Log("左边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = false;
            FlipFalseCollider.SetActive(true);
            FlipTrueCollier.SetActive(false);
        }
        if (Vector3.Dot(transform.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x,  0)) < 0)
        {
         //   Debug.Log("右边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = true;
            FlipFalseCollider.SetActive(false);
            FlipTrueCollier.SetActive(true);
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
           // Debug.Log("左边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = false;
            FlipFalseCollider.SetActive(true);
            FlipTrueCollier.SetActive(false);

            angle = Mathf.Rad2Deg * Mathf.Atan((transform.position.y - _target.position.y) / (transform.position.x - _target.position.x));

            if (transform.position.x - _target.position.x < 0)
            {
                angle = angle - 180;
            }

            bulletAngle = Vector3.Angle(-transform.right, Vector3.up);


        }
        if (Vector3.Dot(Vector3.up, new Vector3(transform.position.y - _target.position.y, transform.position.x - _target.position.x, 0)) < 0)
        {
           // Debug.Log("右边");
            MouthMonstImg.GetComponent<SpriteRenderer>().flipX = true;
            FlipFalseCollider.SetActive(false);
            FlipTrueCollier.SetActive(true);

            angle = Mathf.Rad2Deg * Mathf.Atan((transform.position.y - _target.position.y) / (transform.position.x - _target.position.x));

            if (transform.position.x - _target.position.x < 0)
            {
                //angle = angle - 180;
            }

            bulletAngle = Vector3.Angle(transform.right, Vector3.up);

            bulletAngle = 360 - bulletAngle;
        }

        ShotCtrlList[_index].GetComponent<UbhShotCtrl>()._ShotList[0]._ShotObj.GetComponent<UbhPaintShot>()._PaintCenterAngle = bulletAngle;
        switch (_index)
        {
            case 0:
                ShotCtrlList[_index].GetComponent<UbhShotCtrl>()._ShotList[1]._ShotObj.GetComponent<UbhNwayShot>()._CenterAngle = bulletAngle;
                //ShotCtrlList[_index].GetComponent<UbhShotCtrl>()._ShotList[1]._ShotObj.GetComponent<UbhSinWaveBulletNwayShot>()._CenterAngle = bulletAngle;
                break;
            case 1:
                ShotCtrlList[_index].GetComponent<UbhShotCtrl>()._ShotList[1]._ShotObj.GetComponent<UbhHoleCircleShot>()._HoleCenterAngle = bulletAngle;
                break;
            case 2:
                ShotCtrlList[_index].GetComponent<UbhShotCtrl>()._ShotList[1]._ShotObj.GetComponent<UbhSinWaveBulletNwayShot>()._CenterAngle = bulletAngle;
                break;
        }
        
        

        transform.localEulerAngles = new Vector3(0, 0, angle);
    }

    public void FireOver()
    {
        if (_index < 2)
        {
            _index++;
        }
        else
        {
            _index = 0;
        }
        
        //CutAim();
        //Anim.SetTrigger("Aim");
        //Anim.ResetTrigger("Idle");
        //Anim.ResetTrigger("EnterFire");

        CutDizzy();
        
    }

    private float _dizzyTime;
    void Dizzy()
    {
        if (_dizzyTime < DizzyTime)
        {
            _dizzyTime += Time.deltaTime;
        }
        else
        {
            CutAim();
            _dizzyTime = 0;
            _isHurt = false;
        }
    }

    public void Hurt()
    {
        if (_isHurt)
        {
            StartCoroutine(HurtCoroutine());
        }
    }

    private bool _isHurt;
    IEnumerator HurtCoroutine()
    {
        _bloodNum--;
        _isHurt = false;

        MouthImg.DOColor(Color.red, HurtTime);
        yield return new WaitForSeconds(HurtTime);
        MouthImg.DOColor(Color.white, HurtTime);

        _dizzyTime = DizzyTime + 1;
    }

    //void OnTriggerEnter2D(Collider2D collider)
    //{
    //    //Debug.Log(collider);
    //    if (collider.tag.Equals("Player"))
    //    {
    //        Debug.Log("击中主角");
    //    }
    //    if (collider.tag.Equals("love"))
    //    {
    //        collider.gameObject.SetActive(false);
    //        Debug.Log("打中嘴");
    //    }
    //}
}
