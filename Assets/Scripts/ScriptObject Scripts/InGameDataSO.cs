using UnityEngine;
using System.Collections;
using System.IO;

[CreateAssetMenu(menuName = "InGameItem/InGameDataSO")]
public class InGameDataSO : ScriptableObject
{
    public InGameData inGameData;
    public string jsonFilePath = "InGameData.json"; // JSON 文件路径

    private void OnEnable()
    {
        LoadData();
    }
    [ContextMenu("Load Data")]
    private void LoadData()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, jsonFilePath);

        // 检查文件是否存在
        if (!File.Exists(filePath))
        {
            Debug.LogError($"JSON file does not exist at path: {filePath}");
        }


        string dataAsJson = File.ReadAllText(filePath);

        // 将 JSON 数据解析为对象
        inGameData = JsonUtility.FromJson<InGameData>(dataAsJson);
    }
}
