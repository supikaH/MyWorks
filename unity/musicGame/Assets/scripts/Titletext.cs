using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Titletext : MonoBehaviour {

    public float speed = 1f;

    private Text title;
    private float time = 0;

	// Use this for initialization
	void Start () {
        title = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        title.color = GetAlphaColor(title.color);
	}

    private Color GetAlphaColor(Color color) {
        time += Time.deltaTime * speed;
        color.a = Mathf.Sin(time) * 0.5f + 0.5f;
        return color;
    }
}
