using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerController : MonoBehaviour {
    public float SpeedScale;
    public bool IsJump;
    public Rigidbody2D PlayerRigidbody;
    public Direction CurrentDirection;
    public GameObject BulletPrefab;
    public Vector3 CurrentVector3;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        Move();
        Shoot();
        Roll();
    }

    public void Shoot() {
        if (Input.GetMouseButtonDown(0)) {
            ShootDir();
            AudioManager.PlayerAudioClip(1);
            var bullet = Instantiate(BulletPrefab, transform.position, transform.rotation);
            bullet.GetComponent<BulletController>().InitBullet(CurrentVector3);
        }
    }

    public void Roll() {
        if ((Input.GetAxis("Horizontal") !=0|| Input.GetAxis("Vertical") !=0)&& Input.GetKeyDown(KeyCode.Space)) {
            AudioManager.PlayerAudioClip(2);
            float BeforeSpeed = SpeedScale;
            SpeedScale = SpeedScale * 3;
            DOTween.To(() => SpeedScale, x => SpeedScale = x, BeforeSpeed, 0.3f);
            transform.Translate(Input.GetAxis("Horizontal") * SpeedScale*2, Input.GetAxis("Vertical") * SpeedScale*2, 0);
        }
    }

    public void ShootDir() {
        Vector3 MouseDownPos = Input.mousePosition;
        Vector3 Tar = Camera.main.ScreenToWorldPoint(MouseDownPos);
        float DisX = Tar.x - transform.position.x;
        float DisY = Tar.y - transform.position.y;

        if (Mathf.Abs(DisX) - Mathf.Abs(DisY) > 0) {
            if (DisX > 0) {
                CurrentDirection = Direction.D;
                CurrentVector3 = Vector3.right;
            } else {
                CurrentDirection = Direction.A;
                CurrentVector3 = Vector3.left;
            }
        } else {
            if (DisY > 0) {
                CurrentDirection = Direction.W;
                CurrentVector3 = Vector3.up;
            } else {
                CurrentDirection = Direction.S;
                CurrentVector3 = Vector3.down;
            }
        }
    }

    public void Move() {
        transform.Translate(Input.GetAxis("Horizontal") * SpeedScale, Input.GetAxis("Vertical") * SpeedScale, 0);
    }

    public enum Direction {
        Null = 0,
        W = 1,
        A = 2,
        S = 3,
        D = 4
    }
}
