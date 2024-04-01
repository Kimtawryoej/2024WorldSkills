using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class Json : MonoBehaviour
{
    [ContextMenu("ReadJson")]
    public void ReadJson()
    {
        string jsonData = JsonUtility.ToJson(Ranking.Instance.Data);
        //Debug.Log(jsonData);
        //Debug.Log(Ranking.Instance.Data);
        string path = Path.Combine(Application.streamingAssetsPath, "Data.json");
        File.WriteAllText(path, jsonData);
    }
    [ContextMenu("LoadJson")]
    public void LoadJson()
    {
        string path = Path.Combine(Application.streamingAssetsPath, "Data.json");
        string jsonData = File.ReadAllText(path);
        Ranking.Instance.Data = JsonUtility.FromJson<DataSave>(jsonData);
    }
}
