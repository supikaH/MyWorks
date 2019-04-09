using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(-1)]
public class NotesPool : MonoBehaviour {

    public static NotesPool Instance;

    private List<GameObject> _NotesList = new List<GameObject>();
    private GameObject _baseObj;
    private Vector3 _Pos;

    private void Awake() {
        Instance = this;
    }

    public void CreatePool(GameObject obj,int count) {
        SetPos();
        _baseObj = obj;
        for(int i = 0; i < count; i++) {
            GameObject create = CreateObj(false);
            _NotesList.Add(create);
        }
    }

    private GameObject CreateObj(bool active) {
        GameObject obj = Instantiate(_baseObj);
        obj.transform.position = _Pos;
        obj.SetActive(active);
        return obj;
    }

    public GameObject ReturnObjct() {
        foreach(GameObject obj in _NotesList) {
            if (obj.activeSelf == false) {
                obj.SetActive(true);
                return obj;
            }
        }
        GameObject newObj = CreateObj(true);
        _NotesList.Add(newObj);
        return newObj;
    }

    private void SetPos() {
        _Pos = GameController.Instance.ReturnBasePos();
        _Pos.y += 20;
    }
}
