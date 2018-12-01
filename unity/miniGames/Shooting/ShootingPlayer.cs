using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingPlayer : MonoBehaviour {

    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject BulletObj;

    private float timer = 0;
    [SerializeField]
    private float interlude = 0.5f;

    BulletPool bullets;

    private void Awake() {
        bullets = new BulletPool(20, BulletObj);
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.A)) {
            transform.position += Vector3.left * speed;
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.position += Vector3.right * speed;
        }

        if (Input.GetKeyDown(KeyCode.Space)) {
            GoBullet();
        }
        if (Input.GetKey(KeyCode.Space)) {
            timer += Time.deltaTime;
            if (timer > interlude) {
                GoBullet();
                timer = 0;
            }
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            timer = 0;
        }
	}

    private void OnTriggerEnter(Collider other) {
        string tag = other.transform.tag;
        if (tag != "B_enemy") return;
        //ライフ削減処理
    }

    private void GoBullet() {
        GameObject bullet = bullets.ReturnBullet();
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Bullet>().Shoot();
    }
}
