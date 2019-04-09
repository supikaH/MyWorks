using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayingData {

    private int _Combo = 0;
    private int _Score = 0;
    private int[] _Hanteis = new int[4] { 0, 0, 0, 0 };
    private bool _IsAuto = false;

    public int Combo {
        get { return _Combo; }
    }
    
    public int Score {
        get { return _Score; }
    }
    
    public int[] Hanteis {
        set { _Hanteis = value; }
        get { return _Hanteis; }
    }

    public bool IsAuto {
        set { _IsAuto = value; }
        get { return _IsAuto; }
    }



    public PlayingData() {
        _Combo = 0;
        _Score = 0;
        _Hanteis = new int[4] { 0, 0, 0, 0 };
        _IsAuto = false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="combo"></param>
    /// <param name="score"></param>
    public void SetComboScore(int combo, int score) {
        _Combo = combo;
        _Score = score;
    }
}
