using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingEnemyController : SingletonMonoBehaviour<ShootingEnemyController> {

    [SerializeField]
    GameObject EnemyObj;


    GameObject[] enemys;

    List<GameObject> actives = new List<GameObject>();

    int enemyCount = 5;

    enum enemyTime {
        start,
        playTop,
        play
    }

    float time = 0;

    enemyTime _enemyTime;

    void Awake() {
        base.CreateInstance(this);
    }

    // Use this for initialization
    void Start () {
        enemys = new GameObject[enemyCount];
		for(int i = 0; i < enemyCount; i++) {
            GameObject obj = Instantiate(EnemyObj);
            obj.SetActive(false);
            enemys[i] = obj;
        }
        _enemyTime = enemyTime.playTop;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;

        switch (_enemyTime) {
            case enemyTime.start:
                break;
            case enemyTime.playTop:
                topSet();
                break;
            case enemyTime.play:
                EnemyShoot();
                break;
        }
	}

    void topSet() {
        int setTime = 1;
        if (time > setTime) {
            enemys[0].transform.position = new Vector3(1, 0, 4);
            enemys[0].SetActive(true);
            actives.Add(enemys[0]);
            _enemyTime = enemyTime.play;
        }
    }
    
    private void EnemyShoot() {
        int interlude = 1;
        if (time < interlude) return;
        foreach(GameObject obj in actives) {
            obj.GetComponent<ShootingEnemy>().GoShoot();
        }
        time = 0;
    }

    protected void DesActive(GameObject target) {
        foreach(GameObject obj in actives) {
            if (obj == target) {
                actives.Remove(obj);
                Debug.Log(actives.Count);
            }
        }
    }
}
