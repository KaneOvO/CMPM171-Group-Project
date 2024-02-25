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
    public uint frames = 0;
    public uint maxFrames = 0;
    private double lastInterval;

    [Header("InGame Data Information")]
    [HideInInspector] public InGameData inGameData;
    public SaveData saveData;
    private static GameManager _instance;

    public VoidEventSO OnJsonLoad;
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
        transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
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

        OnJsonLoad.RaiseEvent();
#if UNITY_EDITOR
        Debug.Log("GameManager: Json files loaded");
#endif
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
        saveData.inventory = new List<InventoryItem>();
    }

    private void Update()
    {
        CaculateFPS();
    }
    private void CaculateFPS()
    {
        ++frames;
        maxFrames = Math.Max(maxFrames, frames);
        float timeNow = Time.realtimeSinceStartup;
        if (timeNow > lastInterval + fpsUpdateInterval)
        {
            framesPerSecond = (float)frames / (float)(timeNow - lastInterval);
            frames = 0;
            lastInterval = timeNow;
        }
    }
}
