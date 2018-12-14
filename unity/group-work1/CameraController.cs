using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// カメラコントローラー
/// 親がRoll、子がPitch
/// 親に付ける
/// </summary>
public class CameraController : MonoBehaviour {
    /// <summary>
    /// 親の加算するベクター
    /// </summary>
    [SerializeField]
    private float parAddZ;
    /// <summary>
    /// 子の加算するベクター
    /// </summary>
    [SerializeField]
    private float childAddX;

    [SerializeField]
    private Transform childTransform;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            parAddZ -= 0.5f;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            parAddZ += 0.5f;
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            childAddX += 0.5f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            childAddX -= 0.5f;
        }
        Vector3 pVec = new Vector3(0, 0, parAddZ);
        Vector3 cVec = new Vector3(Mathf.Clamp(childAddX,-90.0f,0.0f), 0, 0);
        transform.localEulerAngles = pVec;
        childTransform.localEulerAngles = cVec;
    }
}
