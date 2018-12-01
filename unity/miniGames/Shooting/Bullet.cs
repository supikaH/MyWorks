using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour {

    [SerializeField]
    private float speed = 10;

    bool isPlayer;


    private void Awake() {
        string tag = transform.tag;
        if (tag == "B_Player") {
            isPlayer = true;
        }
        else if (tag == "B_enemy") {
            isPlayer = false;
        }
    }
    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        CheckOver();
	}

    public void Shoot() {
        if (isPlayer) {
            GetComponent<Rigidbody>().velocity = transform.forward * speed;
        }
        else {
            GetComponent<Rigidbody>().velocity = transform.forward * -speed;
        }
    }

    private void CheckOver() {
        if (isPlayer) {
            if (transform.position.z < 10) return;
        }
        else {
            if (transform.position.z > -10) return;
        }
        this.gameObject.SetActive(false);
    }
}
