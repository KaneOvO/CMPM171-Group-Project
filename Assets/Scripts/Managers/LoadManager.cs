using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LoadManager : MonoBehaviour
{
    public float processValue = 0;
    public AsyncOperation asyncLoad;
    private static LoadManager _instance;
    public static LoadManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<LoadManager>();

                if (_instance == null)
                {
                    GameObject gameManagerObject = new GameObject("LoadSceneManager");
                    _instance = gameManagerObject.AddComponent<LoadManager>();
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
    }

    public void LoadNextScene()
    {
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        asyncLoad = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        asyncLoad.allowSceneActivation = false;
        while (!asyncLoad.isDone)
        {
            processValue = asyncLoad.progress;
            if (asyncLoad.progress >= 0.9f)
            {
                if (Input.anyKeyDown)
                {
                    asyncLoad.allowSceneActivation = true;
                }
            }
            yield return null;
        }
    }
}
