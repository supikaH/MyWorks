using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectButton : MonoBehaviour {

    [SerializeField]
    private string _musicName;

    public string MusicName {
        get { return _musicName; }
    }

    [SerializeField]
    private AudioClip _sabi;

    public AudioClip Sabi {
        get { return _sabi; }
    }

    [SerializeField]
    private AudioClip _GameMusic;

    public AudioClip GameMusic {
        get { return _GameMusic; }
    }

}
