using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {
    public float SpeedScale;
    public Rigidbody2D PlayerRigidbody;
    public Direction CurrentDirection;
    public GameObject BulletPrefab;
    public Vector3 CurrentVector3;

    public Animator HeadAni;
    public Animator BodyAni;
    public string SceneName;
    // Use this for initialization
    void Start() {
        SceneName = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void FixedUpdate() {
        Move();
        Shoot();
        Roll();
        Shock();
    }

    public void Shoot() {
        if (Input.GetMouseButtonDown(0)) {
            ShootDir();
            AudioManager.PlayAudioClip(1);
            var bullet = Instantiate(BulletPrefab, HeadAni.transform.position, transform.rotation);
            bullet.GetComponent<BulletController>().InitBullet(CurrentVector3);
            BackIdle();
        }
    }

    public void Shock() {
        if (Input.GetKeyDown(KeyCode.E)) {
            transform.DOShakePosition(0.5f,0.2f);
        }
    }


    public void BackIdle() {
        StopAllCoroutines();
        StartCoroutine(CountDown());
    }

    public IEnumerator CountDown() {
        yield return new WaitForSeconds(1);
        HeadAni.Play("HeadDown");
        BodyAni.Play("idle");
    }

    public void HeadChange() {

    }

    public void Roll() {
     
        if ((Input.GetAxis("Horizontal") !=0|| Input.GetAxis("Vertical") !=0)&& Input.GetKeyDown(KeyCode.Space)) {
            print("roll!!!!!!   ");
            AudioManager.PlayAudioClip(2);
            float BeforeSpeed = SpeedScale;
            SpeedScale = SpeedScale * 3;
            DOTween.To(() => SpeedScale, x => SpeedScale = x, BeforeSpeed, 0.3f);
            transform.Translate(Input.GetAxis("Horizontal") * SpeedScale*2, Input.GetAxis("Vertical") * SpeedScale*2, 0);
        }
    }

    public void ShootDir() {
        if (SceneName == "Game") {
            Vector3 MouseDownPos = Input.mousePosition;
            Vector3 Tar = Camera.main.ScreenToWorldPoint(MouseDownPos);
            float DisX = Tar.x - transform.position.x;
            float DisY = Tar.y - transform.position.y;

            if (Mathf.Abs(DisX) - Mathf.Abs(DisY) > 0) {

                if (DisX > 0) {
                    CurrentDirection = Direction.D;
                    CurrentVector3 = Vector3.right;
                    HeadAni.Play("HeadRight");
                } else {
                    CurrentDirection = Direction.A;
                    CurrentVector3 = Vector3.left;
                    HeadAni.Play("HeadLeft");
                }
            } else {

                if (DisY > 0) {
                    CurrentDirection = Direction.W;
                    CurrentVector3 = Vector3.up;
                    HeadAni.Play("HeadUp");
                } else {
                    CurrentDirection = Direction.S;
                    CurrentVector3 = Vector3.down;
                    HeadAni.Play("HeadDown");
                }
            }
        }
      
    }

    public void Move() {
        transform.Translate(Input.GetAxis("Horizontal") * SpeedScale, Input.GetAxis("Vertical") * SpeedScale, 0);
        if (Mathf.Abs(Input.GetAxis("Horizontal")) > Mathf.Abs(Input.GetAxis("Vertical"))) {
            BodyAni.Play("Horizontal");
        }
        if (Mathf.Abs(Input.GetAxis("Horizontal")) <Mathf.Abs(Input.GetAxis("Vertical"))) {
            BodyAni.Play("vertical");
        }
    }

    public enum Direction {
        Null = 0,
        W = 1,
        A = 2,
        S = 3,
        D = 4
    }
}
