using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BulletController : MonoBehaviour {
    public float Speed;
    public Vector3 Dir;
    public bool Ready = false;
    // Use this for initialization
    void Start() {

    }

    public IEnumerator ReadyDestroy() {
        yield return new WaitForSeconds(3);
        if (Ready == true) {
            Destroy(gameObject, 0);
        }

    }

    public void InitBullet(Vector3 dir) {
        Dir = dir;
        Ready = true;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Dir != null) {
            gameObject.transform.Translate(Dir * Speed);
        }
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.tag.Equals("Boss"))
        {
            Debug.Log("Boss");

            Destroy(gameObject);
        }

        if (collider.tag.Equals("Wall")) {
            Destroy(gameObject);
        }

        if (collider.tag.Equals("HeartWeak"))
        {
            Debug.Log("HeartWeak");

            transform.parent = collider.transform;
            GetComponent<BoxCollider2D>().enabled = false;
            //Destroy(GetComponent<BoxCollider2D>());
            Ready = false;
            //StopAllCoroutines();
            Speed = 0;
            
            collider.GetComponent<HeartWeak>().Heart.WaterList.Add(gameObject);
        }
    }
}
