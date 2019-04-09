using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : SingletonMonoBehaviour<SoundController> {

    [SerializeField]
    AudioClip[] SE = new AudioClip[3];
    
    AudioClip GameMusic;
    AudioClip _sabiMusic;

    float _MusicPlayTime;

    AudioSource[] audios;

    private void Start() {
        audios = GetComponents<AudioSource>();
    }

    public void MusicStart() {
        audios[0].clip = GameMusic;
        _MusicPlayTime = 0;
        audios[0].time = _MusicPlayTime;
        audios[0].Play();
    }

    public void MusicStop() {
        _MusicPlayTime = audios[0].time;
        audios[0].Stop();
    }

    public void MusicRestart() {
        audios[0].time = _MusicPlayTime;
        audios[0].Play();
    }

    public void MusicReset() {
        audios[0].Stop();
        _MusicPlayTime = 0;
    }

    public void SEStart(int num) {
        audios[1].clip = SE[num];
        audios[1].Play();
    }

    public float ReturnMusicLong() {
        return GameMusic.length;
    }

    public void SabiStart(bool active) {
        if (active) {
            audios[2].clip = _sabiMusic;
            audios[2].loop = true;
            audios[2].Play();
        }
        else {
            audios[2].Stop();
        }
    }

    public void SetMusic(AudioClip all,AudioClip sabi) {
        GameMusic = all;
        _sabiMusic = sabi;
    }
}
