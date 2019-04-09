using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NotesSprite : MonoBehaviour {

    /// <summary>
    /// スピード
    /// </summary>
    public float _speed = 1;
    
    private float MusicTime = 0;
    private float _NotesTime = 0;

    public float NotesTime {
        get { return _NotesTime; }
    }

    private float PosX = 0;

    private float VarY = 0;
    private float TopsY;
    private float LengthY = 0;
    
    private float VarZ = 0;
    private float LengthZ = 0;

    private float Misstime = 0;

    private bool _auto = false;

    // Use this for initialization
    void Start () {
        //レーン長さ取得
        //TopsY = GameController.Instance.ReturnBasePosY();
        //PosX = transform.position.x;
    }
	
	// Update is called once per frame
	void Update () {
        if (GameController.Instance.IsPlaying == false) return;
        
        //曲再生時間取得
        MusicTime = GameController.Instance.ReturnMusicTime();
        //座標反映
        float PosY = VarY + 1 * ((_NotesTime - MusicTime) * LengthY * _speed);
        float PosZ = VarZ + 1 * ((_NotesTime - MusicTime) * LengthZ * _speed);
        transform.position = new Vector3(PosX, PosY, PosZ);

        if (_auto) {
            AutoPlay();
            return;
        }

        if (_NotesTime - MusicTime < Misstime) {
            GameController.Instance.Hantei(3);
            this.gameObject.SetActive(false);
        }
	}

    public void SetUP(float varY,float noteTime,float varZ) {
        TopsY = GameController.Instance.ReturnBasePos().y;
        Misstime = GameController.Instance.MissTime;
        PosX = transform.position.x;
        VarY = varY;
        LengthY = TopsY - varY;
        LengthZ = varZ;
        _NotesTime = noteTime;
        MusicTime = 0;

        _auto = SceneMoveScript.Instance.PlanyerData.IsAuto;
    }

    private void AutoPlay() {
        if (_NotesTime - MusicTime <= 0) {
            GameController.Instance.Hantei(0);
            this.gameObject.SetActive(false);
        }
    }
}
