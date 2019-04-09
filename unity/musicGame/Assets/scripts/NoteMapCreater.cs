using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteMapCreater : MonoBehaviour {

    [SerializeField]
    NoteWriter _baseObj;

    [SerializeField]
    GameObject _var;

    [SerializeField]
    SpriteRenderer _sp;
    

    private float _MusicTime = 0;

    private float minX = 0;
    private float maxX = 0;
    

    // Use this for initialization
    void Start () {
        _MusicTime = SoundController.Instance.ReturnMusicLong();
        SetData();
        _var.transform.position = new Vector3(0, minX, 0);
        
	}
	
	// Update is called once per frame
	void Update () {
        VarMove();
    }

    //public void PushKey(int num) {
    //    if (!_isPlaying) return;
    //    SoundController.Instance.SEStart(0);
    //    _CSVWriter.WriteCSV(GetTiming().ToString() + "," + num.ToString());
    //}

    private void SetData() {
        float size = _sp.bounds.size.x;
        float baseX = size / 2;
        float posY = _sp.gameObject.transform.position.x;

        minX = posY - baseX;
        maxX = posY + baseX;
    }
    
    

    private void VarMove() {
        if (_baseObj.IsPlaying == false) return;
        float spriteSize = maxX - minX;
        float times = _baseObj.GetTiming() / _MusicTime;
        float X = spriteSize * times;

        _var.transform.position = new Vector3(minX + X, 0, 0);
    }
}
