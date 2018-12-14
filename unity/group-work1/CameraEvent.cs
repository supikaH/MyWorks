using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEvent : SingletonMonoBehaviour<CameraEvent> {

    //イベントカメラの仕様
    //イベントポジションとカメラのポジション指定
    //↓
    //LookAtで見る
    
    /// <summary>
    /// 基本のカメラ(デバック用)
    /// </summary>
    [SerializeField]
    private GameObject maincamera;

    /// <summary>
    /// イベント用カメラ格納(デバック用)
    /// </summary>
    [SerializeField]
    private GameObject eventCamera;

    /// <summary>
    /// イベント発生場所(デバック用)
    /// </summary>
    [SerializeField]
    private Vector3 eventPos;

    /// <summary>
    /// イベント時のカメラ場所(デバック用)
    /// </summary>
    [SerializeField]
    private Vector3 cameraPos;

    private Coroutine nowCoroutine;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //デバック用
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartEventCamera(cameraPos, eventPos, 5.0f);
        }
	}

    /// <summary>
    /// カメラ切り替え(デバック用)
    /// </summary>
    private void CameraChange()
    {
        maincamera.SetActive(!maincamera.activeSelf);
        eventCamera.SetActive(!eventCamera.activeSelf);
    }

    /// <summary>
    /// イベント用カメラの位置移動
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="pos"></param>
    private void SetEventCamera(Vector3 pos)
    {
        eventCamera.transform.position = pos;
    }

    /// <summary>
    /// カメラがイベント発生場所を向く
    /// </summary>
    /// <param name="camera"></param>
    /// <param name="pos"></param>
    private void LookEvent(Vector3 pos)
    {
        eventCamera.transform.LookAt(pos, Vector3.back);
    }

    public bool StartEventCamera(Vector3 _thisPos, Vector3 _lookPos, float _lookTime)
    {
        if (nowCoroutine != null) return false;
        CameraChange();
        SetEventCamera(_thisPos);
        LookEvent(_lookPos);
        nowCoroutine = StartCoroutine(LookTimeLimit(_lookTime));
        return true;
    }

    IEnumerator LookTimeLimit(float _lookTime)
    {
        yield return new WaitForSeconds(_lookTime);
        nowCoroutine = null;
        CameraChange();
    }

}
