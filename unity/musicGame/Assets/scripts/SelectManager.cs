using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectManager : MonoBehaviour {

    private string _musicName;

    [SerializeField]
    private Text _titleText;

    [SerializeField]
    private GameObject _startButton;

    private bool _MusicSelected = false;

	// Use this for initialization
	void Start () {
        _MusicSelected = false;
        SetAlphaColor(false);
        SetButtonColor(SceneMoveScript.Instance.PlanyerData.IsAuto = false);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ButtonSelected(SelectButton button) {
        _musicName = button.MusicName;
        _titleText.text = button.gameObject.GetComponentInChildren<Text>().text;
        SoundController.Instance.SetMusic(button.GameMusic, button.Sabi);
        SceneMoveScript.Instance.MusicName = _musicName;
        SoundController.Instance.SabiStart(true);
        SetAlphaColor(true);
        _MusicSelected = true;
    }

    private void SetAlphaColor(bool active) {
        Color color = _startButton.GetComponent<Image>().color;
        if (active) {
            color.a = 1f;
        }
        else {
            color.a = 0.5f;
        }
        _startButton.GetComponent<Image>().color = color;
    }

    public void StartButton() {
        if (!_MusicSelected) return;

        SoundController.Instance.SabiStart(false);
        SceneMoveScript.Instance.ChangeScene("Main");
    }

    public void AutoPlayButton() {
        bool basedata = SceneMoveScript.Instance.PlanyerData.IsAuto;
        bool isAuto = !basedata;
        SetButtonColor(isAuto);
        SceneMoveScript.Instance.PlanyerData.IsAuto = isAuto;
    }

    private void SetButtonColor(bool auto) {
        Color color = _startButton.GetComponent<Image>().color;
        float a = color.a;
        if (auto) {
            color = Color.yellow;
        }
        else {
            color = Color.white;
        }
        color.a = a;
        _startButton.GetComponent<Image>().color = color;
    }
}
