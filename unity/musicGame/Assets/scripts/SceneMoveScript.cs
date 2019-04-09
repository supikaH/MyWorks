using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneMoveScript : SingletonMonoBehaviour<SceneMoveScript> {

    [SerializeField]
    private float _fadeTime = 2;
    private Image _fadePanel;
    
    private string _MusicName = "";

    public string MusicName {
        set { _MusicName = value; }
        get { return _MusicName; }
    }

    private PlayingData _PlayingData;
    public PlayingData PlanyerData {
        get { return _PlayingData; }
    }

    private void Start() {
        _PlayingData = new PlayingData();
        _fadePanel = gameObject.GetComponentInChildren<Image>();
    }

    public void ChangeScene(string name) {
        StartCoroutine(FadeInOut(name));
    }

    private IEnumerator FadeInOut(string name) {
        float start = 0;
        float end = 1;
        Color color = _fadePanel.color;
        
        for(float f = 0; f <= _fadeTime; f += Time.deltaTime) {
            float time = f / _fadeTime;
            color.a = Mathf.Lerp(start, end, time);
            _fadePanel.color = color;
            yield return null;
        }
        SceneManager.LoadScene(name);
        yield return new WaitForSeconds(0.5f);

        color.a = 0;
        _fadePanel.color = color;
    }
}
