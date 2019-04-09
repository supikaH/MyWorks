using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VarScript : MonoBehaviour {

    public int PosNum = 0;
    
    string LineName = "";
    
    GameObject Notes;
    
    private float[] hanteiTime = new float[3];

    bool _Auto = false;

	// Use this for initialization
	void Start () {
        CheckPos(PosNum);
        hanteiTime = GameController.Instance.HanteiTime;
        _Auto = SceneMoveScript.Instance.PlanyerData.IsAuto;
	}
	
	// Update is called once per frame
	void Update () {
        if (_Auto) return;

        if (Input.touchCount > 0) {
            for (int i = 0; i < Input.touchCount; i++) {
                Touch TouchVar = Input.GetTouch(i);
                if (TouchCheak(TouchVar) == false) continue;
                Judgment();
            }
        }
    }

    private void CheckPos(int num) {
        LineName = "Line" + num;
    }
    

    private  void Judgment() {
        GameObject notesobj = surchOBJ();
        if (notesobj == null) return;
        
        float notesTime = notesobj.GetComponent<NotesSprite>().NotesTime;
        float musicTime = GameController.Instance.ReturnMusicTime();

        float interval = Mathf.Abs(notesTime - musicTime);

        if (interval < hanteiTime[0]) {
            GameController.Instance.Hantei(0);
        }
        else if (interval < hanteiTime[1]) {
            GameController.Instance.Hantei(1);
        }
        else if (interval < hanteiTime[2]) {
            GameController.Instance.Hantei(2);
        }
        else return;


        notesobj.SetActive(false);
    }

    /// <summary>
    /// タッチされたのがこのオブジェクトか判断
    /// </summary>
    /// <returns></returns>
    private bool TouchCheak(Touch touch) {
        if (touch.phase != TouchPhase.Began) return false;

        Vector3 pos = touch.position;
        pos.z = 10;

        bool touched = false;
        Vector3 touchPos = Camera.main.ScreenToWorldPoint(pos);
        

        float width = GetComponent<SpriteRenderer>().bounds.size.x;
        float posX = transform.position.x;
        float baseX = width / 2;

        float perXmin = posX - baseX;
        float perXmax = posX + baseX;

        float height = GetComponent<SpriteRenderer>().bounds.size.y;
        float posY = transform.position.y;
        float baseY = height / 2;
        
        float perYmin = posY - baseY;
        float perYmax = posY + baseY;

        if (perXmin <= touchPos.x && touchPos.x <= perXmax) {
            if (perYmin <= touchPos.y && touchPos.y <= perYmax) {
                touched = true;
            }
        }
        

        return touched;
    }

    private GameObject surchOBJ() {
        GameObject gameObj;

        GameObject[] list = GameObject.FindGameObjectsWithTag(LineName);

        if (list.Length <= 0) return null;

        gameObj = list[0];
        float baseY = transform.position.y;
        float objY = Mathf.Abs(gameObj.transform.position.y - baseY);

        for(int i = 1; i < list.Length; i++) {
            float targetY = Mathf.Abs(list[i].transform.position.y - baseY);
            if (targetY < objY) {
                gameObj = list[i];
                objY = targetY;
            }
        }
        
        return gameObj;
    }
}
