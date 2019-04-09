using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Text ComboTex;
    public Text MaxComboTex;
    
    private string NoteName;

    public float TimeOfset = 3f;

    [SerializeField]
    GameObject _NotesObj;
    
    [SerializeField]
    Text HanteiTex;

    [SerializeField]
    Text ScoreText;

    int _score = 0;

    [SerializeField]
    int[] VarXPos = new int[4];
    
    private bool _isPlaying;

    public bool IsPlaying {
        get { return _isPlaying; }
    }

    private bool _startPlay = false;
    
    private int _noteCount = 0;

    [SerializeField]
    private float LoadTime = 3;

    private int NowCombo = 0;
    private int MaxCombo = 0;

    /// <summary>
    /// SCVファイル格納場所(/Notes/)
    /// </summary>
    private string NotePass = "Notes/";


    private List<float> _times = new List<float>();
    private List<int> _notes = new List<int>();

    [SerializeField]
    private float[] hanteiTime = new float[4] { 0, 0, 0, 0 };

    public float MissTime {
        get { return hanteiTime[3] * -1; }
    }

    public float[] HanteiTime {
        get { return hanteiTime; }
    }

    [SerializeField]
    private int baseCount = 20;

    [SerializeField]
    private float startZ = 10;

    private int[] hanteiCount = new int[4];

    private GameObject[] _VarObjs;
    private float[] _VarMinX;
    private float[] _VarMaxX;
    private float[] _varY = new float[2];
    
    bool _autoPlay = false;

    [SerializeField]
    private GameObject AutoText;

    public float _PlayTime = 0;
    

    public static GameController Instance;

    private void Awake() {
        Instance = this;
    }

    // Use this for initialization
    void Start () {
        ResetData();
        LoadCSV();
        SetVarData();
        GetComponent<NotesPool>().CreatePool(_NotesObj, baseCount);
        //PushNotes();
        _autoPlay = SceneMoveScript.Instance.PlanyerData.IsAuto;
        AutoText.SetActive(_autoPlay);
    }
	
	// Update is called once per frame
	void Update () {

        if (_isPlaying) {
            _PlayTime += Time.deltaTime;
        }
        else {
            StartEffect();
        }

        PushNotes2();

        if (ReturnMusicTime() > SoundController.Instance.ReturnMusicLong()) {
            StartCoroutine(EndEffect());
        }
	}

    void LoadCSV() {
        NoteName = SceneMoveScript.Instance.MusicName;
        string file = NotePass + NoteName;
        TextAsset csv = Resources.Load(file) as TextAsset;
        //Debug.Log(csv.text);

        StringReader reader = new StringReader(csv.text);

        while (reader.Peek() > -1) {

            string line = reader.ReadLine();
            string[] value = line.Split(',');
            _times.Add(float.Parse(value[0]));
            _notes.Add(int.Parse(value[1]));
            //Debug.Log(_times[count] + " : " + _notes[count]);
        }
    }

    private void PushNotes() {
        while (_noteCount < _notes.Count) {
            GameObject Notes = Instantiate(_NotesObj);
            Notes.transform.position = new Vector3(PositionX(_notes[_noteCount]), ReturnBasePos().y + 10, startZ);
            Notes.GetComponent<NotesSprite>().SetUP(_VarObjs[0].transform.position.y, _times[_noteCount],startZ);
            Notes.tag = "Line" + _notes[_noteCount].ToString();
            _noteCount++;
        }
    }

    private void PushNotes2() {
        if (_times.Count <= _noteCount) return;
        if (_times[_noteCount] - TimeOfset <= ReturnMusicTime()) {
            GameObject Notes = GetComponent<NotesPool>().ReturnObjct();
            Notes.transform.position = new Vector3(PositionX(_notes[_noteCount]), ReturnBasePos().y + 10, startZ);
            Notes.GetComponent<NotesSprite>().SetUP(_VarObjs[0].transform.position.y, _times[_noteCount], startZ);
            Notes.tag = "Line" + _notes[_noteCount].ToString();
            _noteCount++;
        }
    }

    private int PositionX(int Base) {
        int pos = 0;

        pos = VarXPos[Base];

        return pos;
    }

    public float ReturnMusicTime() {
        //return Time.time - _startTime;
        return _PlayTime;
    }

    public Vector3 ReturnBasePos() {
        Vector3 max = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width,Screen.height,startZ));
        return max;
    }


    /// <summary>
    /// 判定表示(内部でスコア、コンボ計算)
    /// 0:perfect 1:good 2:bad 3:miss
    /// </summary>
    /// <param name="H"></param>
    public void Hantei(int H) {
        switch (H) {
            case 0:
                //perfect
                HanteiTex.text = "PERFECT";
                hanteiCount[0]++;
                SoundController.Instance.SEStart(0);
                ChangeCombo(true);
                AddScore(500);
                break;
            case 1:
                //good
                hanteiCount[1]++;
                HanteiTex.text = "GOOD";
                SoundController.Instance.SEStart(1);
                ChangeCombo(true);
                AddScore(250);
                break;
            case 2:
                //bad
                hanteiCount[2]++;
                HanteiTex.text = "BAD";
                SoundController.Instance.SEStart(2);
                ChangeCombo(false);
                AddScore(100);
                break;
            case 3:
                //miss
                hanteiCount[3]++;
                HanteiTex.text = "MISS";
                ChangeCombo(false);
                break;
        }
        SetComboText();
    }

    private void SetComboText() {
        ComboTex.text = NowCombo.ToString();
        //MaxComboTex.text = MaxCombo.ToString();
    }

    /// <summary>
    /// コンボが切れた時、最大コンボの更新を行う
    /// </summary>
    private void ChangeCombo(bool isCombo) {

        if (isCombo) {
            NowCombo++;
        }

        if (MaxCombo < NowCombo) {
            MaxCombo = NowCombo;
        }

        if (!isCombo) {
            NowCombo = 0;
        }
    }

    private void StartEffect() {
        if (_startPlay) return;


        LoadTime -= Time.deltaTime;
        HanteiTex.text = Mathf.FloorToInt(LoadTime).ToString();

        if (LoadTime < 0) {
            HanteiTex.text = "";
            SoundController.Instance.MusicReset();
            SoundController.Instance.MusicStart();
            _isPlaying = true;
            _startPlay = true;
        }
    }

    private void AddScore(int num) {
        _score += num;
        ScoreText.text = _score.ToString("D11");
    }

    private IEnumerator EndEffect() {
        if (_noteCount == MaxCombo) {
            HanteiTex.text = "フルコン!!";
        }
        //yield return new WaitForSeconds(3);
        
        SceneMoveScript.Instance.PlanyerData.SetComboScore(MaxCombo, _score);
        SceneMoveScript.Instance.PlanyerData.Hanteis = hanteiCount;

        //ResetData();
        yield return null;

        SceneMoveScript.Instance.ChangeScene("Result");
    }

    private void SetVarData() {
        _VarObjs = GameObject.FindGameObjectsWithTag("Var");
        _VarMinX = new float[_VarObjs.Length];
        _VarMaxX = new float[_VarObjs.Length];
        int count = 0;
        foreach(GameObject var in _VarObjs) {
            float width = var.GetComponent<SpriteRenderer>().bounds.size.x;
            float posX = var.transform.position.x;
            float baseX = width / 2;

            _VarMinX[count] = posX - baseX;
            _VarMaxX[count] = posX + baseX;
            count++;
        }
        Array.Sort(_VarMinX);
        Array.Sort(_VarMaxX);

        //yはすべて同じ位置にあるものとする
        float height = _VarObjs[0].GetComponent<SpriteRenderer>().bounds.size.y;
        float posY = _VarObjs[0].transform.position.y;
        float baseY = height / 2;

        _varY[0] = posY - baseY;
        _varY[1] = posY + baseY;

    }

    /// <summary>
    /// syokikasyori
    /// </summary>
    private void ResetData() {
        SoundController.Instance.MusicReset();

        _isPlaying = false;
        _startPlay = false;
        _PlayTime = 0;
        _noteCount = 0;
        hanteiCount = new int[4] { 0, 0, 0, 0 };
        MaxCombo = 0;
        _score = 0;
        NowCombo = 0;

        _times = new List<float>();
        _notes = new List<int>();

    }

    /// <summary>
    /// true=playing false=not playing
    /// </summary>
    /// <param name="play"></param>
    public void StopPlay(bool play) {
        _isPlaying = play;
        if (play) {
            SoundController.Instance.MusicRestart();
        }
        else {
            SoundController.Instance.MusicStop();
        }
    }
}
