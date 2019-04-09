using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoteWriter : MonoBehaviour {
    
    [SerializeField]
    CSVWriter _CSVWriter;
    [SerializeField]
    private GameObject startButton;
    [SerializeField]
    private AudioClip _music;

    [SerializeField]
    Text TimeTex;

    private bool _isPlaying;
    public bool IsPlaying {
        get { return _isPlaying; }
    }

    private float _startTime = 0;

    public float StartTime {
        get { return _startTime; }
    }
    

	// Use this for initialization
	void Start () {
        SoundController.Instance.SetMusic(_music, null);
    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            if (_isPlaying) return;
            StartButton();
        }

        if (!_isPlaying) return;

        if (Input.GetKeyDown(KeyCode.A)) {
            PushKey(0);
        }
        if (Input.GetKeyDown(KeyCode.S)) {
            PushKey(1);
        }
        if (Input.GetKeyDown(KeyCode.K)) {
            PushKey(2);
        }
        if (Input.GetKeyDown(KeyCode.L)) {
            PushKey(3);
        }

        TimeTex.text = GetTiming().ToString();
    }

    public void StartButton() {
        startButton.SetActive(false);
        SoundController.Instance.MusicStart();
        _startTime = Time.time;
        _isPlaying = true;
    }

    public  void PushKey(int num) {
        if (!_isPlaying) return;
        SoundController.Instance.SEStart(0);
        _CSVWriter.WriteCSV(GetTiming().ToString() + "," + num.ToString());
    }

    public float GetTiming() {
        return Time.time - _startTime;
    }
}
