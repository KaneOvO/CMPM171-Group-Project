using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Networking;
using System.IO;
using UnityEditor;
using UnityEngine.Events;
using System;
[AddComponentMenu("Managers/GameManager")]
public class GameManager : MonoBehaviour
{
    [Header("Game Information")]
    [Range(0f, 1f)] public float fpsUpdateInterval = 0.5f;
    public float framesPerSecond = 0;
    private int frames = 0;
    private double lastInterval;

    [Header("InGame Data Information")]
    [Range(0, 30)] public int endDay = 3;
    [HideInInspector]public InGameData inGameData;
    public SaveData saveData;
    private static GameManager _instance;

    public event Action OnJsonLoad;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameManager>();

                if (_instance == null)
                {
                    GameObject gameManagerObject = new GameObject("GameManager");
                    _instance = gameManagerObject.AddComponent<GameManager>();
                }
            }
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
        // DontDestroyOnLoad(gameObject);
    }

    public void Pause()
    {
        Time.timeScale = 0;
    }

    public void Continue()
    {
        Time.timeScale = 1;
    }
    private IEnumerator Start()
    {
        lastInterval = Time.realtimeSinceStartup;

        yield return LoadJsonFilesAsync();

        OnJsonLoad?.Invoke();
        Debug.Log($"{Global.NOTIFICATION}");
    }

    private IEnumerator LoadJsonFilesAsync()
    {
        string inGameDataFilePath = Path.Combine(Application.streamingAssetsPath, "InGameData.json");
        yield return LoadJsonFileAsync<InGameData>(inGameDataFilePath, (data) => inGameData = data);

        string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");
        yield return LoadJsonFileAsync<SaveData>(saveDataFilePath, (data) => saveData = data);
    }

    private IEnumerator LoadJsonFileAsync<T>(string filePath, Action<T> onDataLoaded)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(filePath);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError($"Error loading JSON file from {filePath}: {webRequest.error}");
        }
        else
        {
            string dataAsJson = webRequest.downloadHandler.text;
            T data = JsonUtility.FromJson<T>(dataAsJson);
            onDataLoaded?.Invoke(data);
        }
        webRequest.Dispose();
    }

    private void InitializedSaveData()
    {
        saveData = new SaveData();
        saveData.currentDay = 1;
        saveData.playerState = inGameData.initialDatas.playerState;
        saveData.inventory = new Dictionary<string, int>();
    }

    private void Update()
    {
        CaculateFPS();
    }
    private void CaculateFPS()
    {
        ++frames;
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + fpsUpdateInterval)
        {
            framesPerSecond = (float)(frames / (timeNow - lastInterval));
            frames = 0;
            lastInterval = timeNow;
        }
    }
}
