using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraParent : MonoBehaviour {

    //現在のステージデータ格納
    private StageData nowSD;
    //マップ中心座標取得
    private float mapX, mapY;
    //トップビュー時のZ座標計算
    [SerializeField]
    private float topZ = -10;
    [SerializeField]
    private float offset = 0;
    //
    public float distanceCamera = 2;
    //private int countQuarter = 0;
    [SerializeField]
    private float quaZ = -10;
    private Vector3 centerVec;

    private Transform child;

    private bool cameraTopView =true;

    [SerializeField]
    float roteTime = 1f;

    [SerializeField]
    Camera childCamera;
    [SerializeField]
    float cameraSens = 10f;
    
    [SerializeField]
    float mouseFreeRote = 5f;

    //CameraPitchを入れる
    [SerializeField]
    GameObject cameraZ;

    //カメラ回転制限
    [SerializeField]
    float limitRotate = 25;

    Coroutine nowTurn;

    
    GameObject[] walls = null;

    // Use this for initialization
    void Start () {
        if (StageController.instance.isTutorial) { nowSD = StageSetList.instance.GetTutorialStageData(); }
        else { nowSD = StageSetList.instance.GetStageData(); }
        InputStageData();
        transform.position = new Vector3(mapX, mapY, offset);
        TopView();
        centerVec = new Vector3(mapX, mapY, quaZ);
    }
	
	// Update is called once per frame
	void Update ()
    {
        UpdateWall();
        if (nowTurn != null) return;

        FreeTopRotate();

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SetZoom();
            TopView();
            //WallActiveFalse(true);
        }

        if (cameraTopView && Input.GetKeyDown(KeyCode.DownArrow))
        {
            SetZoom();
            child.transform.position = (centerVec + new Vector3(mapX, mapY) * distanceCamera);

            child.transform.LookAt(new Vector3(mapX, mapY, offset), new Vector3(0, 0, -1));
            //WallActiveFalse();
            cameraTopView = false;
        }
        CameraZoom();


        //トップビューの場合は回転しない
        if ((Vector2)child.transform.localPosition == Vector2.zero ) { return; }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            nowTurn = StartCoroutine(Turn(false));
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            nowTurn = StartCoroutine(Turn(true));
        }

        //FreeRotate();
    }

    void UpdateWall() {
        if (walls == null) { walls = StageGenerator.instance.Walls; }
        TopViewNoneActiveObject.SetActive(false);

        if (cameraZ.transform.eulerAngles.z == 0) {
            walls[0].SetActive(true);
            walls[1].SetActive(true);
            walls[2].SetActive(true);
            walls[3].SetActive(true);
            return;
        }

        //一番角度の大きい方向の壁を消す
        if (Mathf.Abs(child.position.x) > Mathf.Abs(child.position.y))
        {
            walls[child.position.x > 0 ? 0 : 1].SetActive(true);
            walls[child.position.x > 0 ? 1 : 0].SetActive(false);
        }
        else if (child.transform.position.y != 0)
        {
            walls[child.position.y > 0 ? 2 : 3].SetActive(true);
            walls[child.position.y > 0 ? 3 : 2].SetActive(false);
        }
    }

    /// <summary>
    /// 座標データ取得
    /// </summary>
    private void InputStageData()
    {
        //子オブジェクト取得
        child = Camera.main.transform;

        //stageDate取得
        //nowSD = StageController.nowStageData;
        //mapSizeX = nowSD.sizeX;
        //mapSizeY = nowSD.sizeY;

        //map中心座標計算
        mapX = (nowSD.sizeX + 1f) / 2f;
        mapY = (nowSD.sizeY + 1f) / 2f;
    }



    /// <summary>
    /// トップビュー表示
    /// </summary>
    private void TopView()
    {
        child.transform.position = new Vector3(mapX, mapY, topZ);
        transform.rotation = new Quaternion(0, 0, 0, 0);
        child.transform.rotation = new Quaternion(0, 0, 0, 0);
        //countQuarter = 0;
        cameraTopView = true;
    }
    
    IEnumerator Turn(bool _roteDis)
    {
        int add = _roteDis ? 90 : -90;
        Quaternion targetQua = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z + add);
        Quaternion myQua = transform.rotation;
        float timer = 0;
        while(timer <= roteTime)
        {
            transform.rotation = Quaternion.Slerp(myQua, targetQua, timer / roteTime);
            timer += Time.deltaTime;
            yield return null;
        }
        transform.rotation = targetQua;
        nowTurn = null;
        //if (!cameraTopView) {WallActiveFalse(); }
        yield break;
    }
    /// <summary>
    /// スクリーン上で下にある二つの壁のアクティブを外す
    /// </summary>
    /// <param name="wallActiveFlag">引数にtrueを入れると壁の全アクティブだけをする</param>
    //private void WallActiveFalse(bool wallActiveFlag = false)
    //{
    //    if (wallActiveFlag == true)
    //    {

    //        //for (int i = 0; i < walls.Length; i++)
    //        //{
    //        //    walls[i].SetActive(true);
    //        //}

    //        return;
    //    }
    //    int[] x = new int[2];
    //    float[] min = new float[2];
    //    min[0] = 10000;
    //    min[1] = 10000;
    //    float[] y = new float[4];
    //    for (int i = 0; i < walls.Length; i++)
    //    {
    //        walls[i].SetActive(true);
    //        y[i] = Camera.main.WorldToScreenPoint(walls[i].transform.position).y;
    //        if (min[0] > y[i])
    //        {
    //            min[1] = min[0];
    //            min[0] = y[i];
    //            x[1] = x[0];
    //            x[0] = i;
    //        }
    //        else if (min[1] > y[i])
    //        {
    //            min[1] = y[i];
    //            x[1] = i;
    //        }
    //    }
    //    for (int i = 0; i < x.Length; i++)
    //    {
    //        walls[x[i]].SetActive(false);
    //    }
    //}


    /// <summary>
    /// カメラのズーム
    /// </summary>
    private void CameraZoom()
    {
        float getMouse = Input.GetAxis("Mouse ScrollWheel");
        childCamera.fieldOfView = Mathf.Clamp(childCamera.fieldOfView + (getMouse * cameraSens), 20, 100);
    }

    
    /// <summary>
    /// カメラ切り替え時のズーム初期化
    /// </summary>
    private void SetZoom()
    {
        childCamera.fieldOfView = 65;
    }

    /// <summary>
    /// 右クリックで自由回転
    /// </summary>
    private void FreeRotate()
    {
        if (!Input.GetMouseButton(1)) return;
        float mouseX = Input.GetAxis("Mouse X");
        transform.Rotate(0, 0, transform.rotation.z + (-mouseX * mouseFreeRote));
    }

    /// <summary>
    /// 右クリックで自由回転(トップビュー)
    /// </summary>
    private void FreeTopRotate()
    {
        if (Input.GetMouseButton(1))
        {
            float moveX = Input.GetAxis("Mouse X");
            float moveY = Input.GetAxis("Mouse Y");
            if (Mathf.Abs(moveX) < Mathf.Abs(moveY))
            {
                float vecX = cameraZ.transform.rotation.x + (-moveY * mouseFreeRote);
                float limit = -limitRotate * Mathf.Deg2Rad;
                if (vecX > 0f && cameraZ.transform.localRotation.x > 0) return;
                if (vecX < 0f && cameraZ.transform.localRotation.x < limit) return;
                cameraZ.transform.Rotate(vecX, 0, 0);
            }
            else if(Mathf.Abs(moveX)!= Mathf.Abs(moveY))
            {
                transform.Rotate(0, 0, transform.rotation.z + (-moveX * mouseFreeRote));
            }
            
            
        }
    }
}
