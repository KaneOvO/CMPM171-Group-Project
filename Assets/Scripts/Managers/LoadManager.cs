using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
public class LoadManager : MonoBehaviour
{
    public GameSceneSO firstGameSceneSO;
    [Header("Events listener:load new scene")]
    public SceneLoadEventSO sceneLoadEvent;
    [Header("Events sender:load new scene completed")]
    public VoidEventSO sceneLoadCompletedEvent;
    [Header("Events sender:fade canvas")]
    public FadeEventSO fadeEvent;
    private static LoadManager _instance;
    [SerializeField] private GameSceneSO currentSceneSO;
    [SerializeField] private GameSceneSO sceneToGoSO;
    private bool fade = false;
    private float fadeDuration = 0.5f;
    private bool isLoading = false;
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
        transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
    }
    // Register this class as listener to the SceneLoadEventSO
    private void OnEnable()
    {
        sceneLoadEvent.onEventRaised += OnLoadRequestedEvent;
    }

    private void OnDisable()
    {
        sceneLoadEvent.onEventRaised -= OnLoadRequestedEvent;
    }

    public void OnLoadRequestedEvent(GameSceneSO sceneSO, bool fade)
    {
        if (isLoading) return;
        isLoading = true;
        sceneToGoSO = sceneSO;
        this.fade = fade;

        StartCoroutine(UnloadPreviousScene());
    }

    public void Start()
    {
        sceneToGoSO = firstGameSceneSO;
        LoadNewScene();
    }
    private IEnumerator UnloadPreviousScene()
    {
        if (fade)
        {
            //TODO: DO fade 
            fadeEvent.FadeIn(fadeDuration);
        }
        yield return new WaitForSeconds(fadeDuration);
        yield return currentSceneSO.sceneReference.UnLoadScene();
        LoadNewScene();
    }
    public void LoadNewScene()
    {
        var loadingOption = sceneToGoSO.sceneReference.LoadSceneAsync(LoadSceneMode.Additive, true);
        loadingOption.Completed += OnSceneLoadComplete;
    }
    private void OnSceneLoadComplete(AsyncOperationHandle<SceneInstance> obj)
    {

        currentSceneSO = sceneToGoSO;
        if (fade)
        {
            //TODO: DO fade
            fadeEvent.FadeOut(fadeDuration);
        }
        isLoading = false;
        sceneLoadCompletedEvent.RaiseEvent();
    }
}
