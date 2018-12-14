using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFPS : MonoBehaviour {

    private Transform XRot;
    private Transform YRot;

    [SerializeField]
    private float sensitivity = 1.5f;
    [SerializeField]
    private float speed = 1.5f;

    // Use this for initialization
    void Start () {
        transform.parent.position = new Vector3(0, 0, -10);
        XRot = transform.parent;
        YRot = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        float X_rot = Input.GetAxis("Mouse X");
        float Y_rot = Input.GetAxis("Mouse Y");
        XRot.transform.Rotate(0, X_rot * sensitivity, 0);
        YRot.transform.Rotate(-Y_rot * sensitivity, 0, 0);
        

        if (Input.GetKey(KeyCode.W))
        {
            transform.parent.position += transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.parent.position -= transform.forward * speed;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.parent.position += transform.right * speed;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.parent.position -= transform.right * speed;
        }
        
    }
}
