using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Networking;
using System.IO;

public class GameManager : MonoBehaviour
{
    public int currentDay = -1;
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }
    public InGameData inGameData;
    public SaveData saveData;
    private IEnumerator Start()
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, "InGameData.json");

        using (UnityWebRequest webRequest = UnityWebRequest.Get(filePath))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string dataAsJson = webRequest.downloadHandler.text;
                inGameData = JsonUtility.FromJson<InGameData>(dataAsJson);
                ItemManager.Instance.items = inGameData.items;
            }
        }

        filePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(filePath))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error: " + webRequest.error);
            }
            else
            {
                string dataAsJson = webRequest.downloadHandler.text;
                saveData = JsonUtility.FromJson<SaveData>(dataAsJson);
                currentDay = saveData.currentDay;
                PlayerStateManager.Instance.playerState = saveData.playerState;
            }
        }
    }
}
