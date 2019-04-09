using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CSVWriter : MonoBehaviour {

    public string NoteName;

    /// <summary>
    /// SCVファイル格納場所(/Resources/Notes/)
    /// </summary>
    private string NotePass = "/Resources/Notes/";

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void WriteCSV(string str) {
        StreamWriter sw;
        FileInfo file;
        file = new FileInfo(Application.dataPath + NotePass + NoteName + ".csv");
        sw = file.AppendText();
        sw.WriteLine(str);
        sw.Flush();
        sw.Close();
    }
}
