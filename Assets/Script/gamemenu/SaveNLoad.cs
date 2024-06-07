using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class SaveData
{
    public Vector3 playerPos;
    public Vector3 playerRot;
    public bool hasPhone;
    public bool[] hasKeys;
    public int Messagenum;
    public int notenum;
}

public class SaveNLoad : MonoBehaviour
{
    private SaveData saveData = new SaveData();


    private string SAVE_DATA_DIRECTORY;
    private string SAVE_FILENAME = "/SaveFile.txt";

    private PlayerController thePlayer;

    // Start is called before the first frame update
    void Start()
    {

        SAVE_DATA_DIRECTORY = Application.dataPath + "/Saves/";

        if (!Directory.Exists(SAVE_DATA_DIRECTORY))
            Directory.CreateDirectory(SAVE_DATA_DIRECTORY);
    }

    public void SaveDate()
    {
        thePlayer = FindObjectOfType<PlayerController>();

        saveData.playerPos = thePlayer.transform.position;
        saveData.playerRot = thePlayer.transform.eulerAngles;
        saveData.hasPhone = thePlayer.hasPhone;
        saveData.hasKeys = thePlayer.hasKeys;
        saveData.Messagenum = thePlayer.Messagenum;
        saveData.notenum = thePlayer.notenum;

        string json = JsonUtility.ToJson(saveData);

        File.WriteAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME, json);

        Debug.Log("저장 완료");
        Debug.Log(json);
    }

    public void LoadData()
    {
        if (File.Exists(SAVE_DATA_DIRECTORY + SAVE_FILENAME))
        {
            string loadJson = File.ReadAllText(SAVE_DATA_DIRECTORY + SAVE_FILENAME);
            saveData = JsonUtility.FromJson<SaveData>(loadJson);

            thePlayer = FindObjectOfType<PlayerController>();

            thePlayer.transform.position = saveData.playerPos;
            thePlayer.transform.eulerAngles = saveData.playerRot;
            thePlayer.hasPhone = saveData.hasPhone;
            thePlayer.hasKeys = saveData.hasKeys;
            thePlayer.Messagenum = saveData.Messagenum;
            thePlayer.notenum = saveData.notenum;

            Debug.Log("로드 완료");
        }
        else
        {
            Debug.Log("세이브 파일이 없습니다.");
        }
    }
}
