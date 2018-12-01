using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPool {

    private List<GameObject> Bullets = new List<GameObject>();
    private GameObject BulletObj;
    
    public BulletPool(int count, GameObject obj) {
        FirstCreate(count, obj);
    }


    private void FirstCreate(int count, GameObject baseObj) {
        BulletObj = baseObj;
        for(int i = 0; i < count; i++) {
            GameObject bullet = CreateBullet();
            bullet.SetActive(false);
            Bullets.Add(bullet);
        }
    }

    public GameObject ReturnBullet() {
        for(int i = 0; i < Bullets.Count; i++) {
            if (Bullets[i].activeSelf == false) {
                Bullets[i].SetActive(true);
                return Bullets[i];
            }
        }

        GameObject bullet = CreateBullet();
        bullet.SetActive(true);
        Bullets.Add(bullet);
        return bullet;
    }

    private GameObject CreateBullet() {
        GameObject Obj = MonoBehaviour.Instantiate(BulletObj);
        Obj.name = BulletObj.name + (Bullets.Count + 1);
        return Obj;
    }
}
