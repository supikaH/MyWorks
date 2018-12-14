using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChange : MonoBehaviour {

    //現在のステージデータ格納
    public StageData nowSD;
    //マップ中心座標取得
    private float mapX, mapY,mapSizeX,mapSizeY;
    //トップビュー時のZ座標計算
	[SerializeField]
	private float topZ = -10;
    //クォータービューの座標格納
    public float distanceCamera = 2;
    [SerializeField]
    private List<Vector3> quarter = new List<Vector3>();
    private int countQuarter = 0;
    int count;
	[SerializeField]
	private float quaZ = -10;


    // Use this for initialization
    void Start () {
        inputStageData();
        SetDic();
        //スタート時はトップビュー
        topView();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.UpArrow)) topView();

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            transform.position = quarter[(int)Mathf.Repeat(countQuarter, quarter.Count)];
            transform.LookAt(new Vector3(mapX, mapY, 0), new Vector3(0, 0, -1));
        }


        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (transform.position == new Vector3(mapX, mapY, topZ)) return;
            count = (int)Mathf.Repeat(++countQuarter, quarter.Count);
            StartCoroutine("turnQuater");
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (transform.position == new Vector3(mapX, mapY, topZ)) return;
            count = (int)Mathf.Repeat(--countQuarter, quarter.Count);
            StartCoroutine("turnQuater");
        }
    }

    /// <summary>
    /// 座標データ取得
    /// </summary>
    private void inputStageData()
    {
        if (!nowSD) { return; }
        //stageDate取得
        //nowSD = StageController.nowStageData;
        mapSizeX = nowSD.sizeX;
        mapSizeY = nowSD.sizeY;

        //map中心座標計算
        mapX = nowSD.sizeX / 2;
        mapY = nowSD.sizeY / 2;
    }

    /// <summary>
    /// ステージサイズの更新
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    public void SetStageSize(int x,int y) {
        mapSizeX = x;
        mapSizeY = y;

        mapX = mapSizeX / 2;
        mapY = mapSizeY / 2;

        SetDic();
    }

    /// <summary>
    /// クォータービュー座標取得
    /// </summary>
    private void SetDic()
    {
        quarter.Clear();

		quarter.Add(new Vector3(mapSizeX * distanceCamera, mapSizeY * distanceCamera, quaZ));
		quarter.Add(new Vector3(mapSizeX * distanceCamera, -mapSizeY * distanceCamera + mapSizeY, quaZ));
		quarter.Add(new Vector3(-mapSizeX * distanceCamera + mapSizeX, -mapSizeY * distanceCamera + mapSizeY, quaZ));
		quarter.Add(new Vector3(-mapSizeX * distanceCamera + mapSizeX, mapSizeY * distanceCamera, quaZ));
    }

    /// <summary>
    /// トップビュー表示
    /// </summary>
    private void topView()
    {
        transform.position = new Vector3(mapX, mapY, topZ);
        transform.rotation = new Quaternion(0, 0, 0, 0);
    }

    /// <summary>
    /// クォータービュー
    /// </summary>
    IEnumerator turnQuater()
    {
        for(int f = 1; f <= 60; f++)
        {
            transform.position = Vector3.Slerp(transform.position, quarter[count], 0.1f);
            transform.LookAt(new Vector3(mapX, mapY, 0), Vector3.back/* new Vector3(0, 0, -1)*/);
            yield return null;
        }
    }

    
}
