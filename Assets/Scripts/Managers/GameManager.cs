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
    [Header("Mouse Position")]
    public Vector3 mousePosition;
    [Header("Game Information")]
    [Range(0f, 1f)] public float fpsUpdateInterval = 0.5f;
    public float framesPerSecond = 0;
    public uint frames = 0;
    public uint maxFrames = 0;
    private double lastInterval;

    [Header("InGame Data Information")]
    [HideInInspector] public InGameData inGameData;
    public SaveData saveData;
    public PlayerConfig playerConfig;
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
        LoadDataFromPlayerPrefs();

        yield return LoadJsonFilesAsync();

        OnJsonLoad.RaiseEvent();
#if UNITY_EDITOR
        Debug.Log("GameManager: Json files loaded");
#endif
    }

    private IEnumerator LoadJsonFilesAsync()
    {
        string inGameDataFilePath = Path.Combine(Application.streamingAssetsPath, "InGameData.json");
        StartCoroutine(LoadJsonFileAsync<InGameData>(inGameDataFilePath, (data) => inGameData = data));

        // string playerConfigFilePath = Path.Combine(Application.streamingAssetsPath, "PlayerConfig.json");
        // yield return LoadJsonFileAsync<PlayerConfig>(playerConfigFilePath, (data) => playerConfig = data);

        // string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");
        // yield return LoadJsonFileAsync<SaveData>(saveDataFilePath, (data) => saveData = data);

        yield return null;
    }

    private void LoadDataFromPlayerPrefs()
    {
        // Loading PlayerConfig
        if (PlayerPrefs.HasKey("PlayerConfig"))
        {
            string playerConfigJson = PlayerPrefs.GetString("PlayerConfig");
            playerConfig = JsonUtility.FromJson<PlayerConfig>(playerConfigJson);
#if UNITY_EDITOR
            Debug.Log("PlayerConfig loaded");
#endif
        }
        else
        {
            playerConfig = new PlayerConfig
            {
                currentLanguage = Language.English,
                volume = 1f
            };
            Save();
#if UNITY_EDITOR
            Debug.Log("PlayerConfig created");
#endif
        }
    }

    public IEnumerator LoadJsonFileAsync<T>(string filePath, Action<T> onDataLoaded)
    {
        UnityWebRequest webRequest = UnityWebRequest.Get(filePath);
        yield return webRequest.SendWebRequest();

        if (webRequest.result == UnityWebRequest.Result.ProtocolError)
        {
#if UNITY_EDITOR
            Debug.LogError($"Error loading JSON file from {filePath}: {webRequest.error}");
#endif
            yield break;
        }
        else
        {
            string dataAsJson = webRequest.downloadHandler.text;
            T data = JsonUtility.FromJson<T>(dataAsJson);
            onDataLoaded?.Invoke(data);
        }
        webRequest.Dispose();
    }

    private void Update()
    {
        CaculateFPS();
        mousePosition = Input.mousePosition;
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
    // private void OnApplicationQuit()
    // {
    //     Save();
    // }
    // public void Save(bool hardSave = false)
    // {
    //     string playerConfigFilePath = Path.Combine(Application.streamingAssetsPath, "PlayerConfig.json");
    //     string dataAsJson = JsonUtility.ToJson(playerConfig);
    //     File.WriteAllText(playerConfigFilePath, dataAsJson);
    //     string saveDataFilePath = Path.Combine(Application.streamingAssetsPath, "SaveData.json");
    //     if (File.Exists(saveDataFilePath)||hardSave)
    //     {
    //         dataAsJson = JsonUtility.ToJson(saveData);
    //         File.WriteAllText(saveDataFilePath, dataAsJson);
    //     }
    // }

    public void Save(bool hardSave = false)
    {
        string playerConfigJson = JsonUtility.ToJson(playerConfig);
        PlayerPrefs.SetString("PlayerConfig", playerConfigJson);
        string saveDataJson = JsonUtility.ToJson(saveData);
        if (PlayerPrefs.HasKey("SaveData") || hardSave)
        {
            PlayerPrefs.SetString("SaveData", saveDataJson);
        }

        PlayerPrefs.Save();
    }
    [ContextMenu("Clear PlayerPrefs")]
    public void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
}
