using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemy : ShootingEnemyController {

    BulletPool E_bullets;

    [SerializeField]
    protected GameObject E_Bullet;

    private void Awake() {
        E_bullets = new BulletPool(10, E_Bullet);
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other) {
        tag = other.transform.tag;
        if (tag != "B_Player") return;
        base.DesActive(this.gameObject);
        this.gameObject.SetActive(false);
    }

    public void GoShoot() {
        GameObject bullet = E_bullets.ReturnBullet();
        bullet.transform.position = this.transform.position;
        bullet.GetComponent<Bullet>().Shoot();
    }
    
}
