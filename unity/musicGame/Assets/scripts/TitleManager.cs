using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount <= 0) return;
        if (Input.GetTouch(0).phase == TouchPhase.Began) {
            TouchScreen();
        }
	}

    private void TouchScreen() {
        SoundController.Instance.SEStart(0);
        SceneMoveScript.Instance.ChangeScene("Select");
    }
}
