using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour {

    [SerializeField]
    private Text _scoreTex;
    [SerializeField]
    private Text _ComboTex;
    [SerializeField]
    private Text[] HanteiTexts = new Text[4];

	// Use this for initialization
	void Start () {
        SetText();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.touchCount > 0) {
            SceneMoveScript.Instance.ChangeScene("Select");
        }
	}

    private void SetText() {
        _scoreTex.text = SceneMoveScript.Instance.PlanyerData.Score.ToString();
        _ComboTex.text = SceneMoveScript.Instance.PlanyerData.Combo.ToString();
        int[] h = SceneMoveScript.Instance.PlanyerData.Hanteis;
        for (int i = 0; i < HanteiTexts.Length; i++) {
            HanteiTexts[i].text = h[i].ToString();
        }
    }
}
