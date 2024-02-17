using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
[AddComponentMenu("Managers/InGameEventManager")]
public class InGameEventManager : MonoBehaviour
{
    public int currentDay => GameManager.Instance.saveData.currentDay;
    public Stage currentStage => GameManager.Instance.saveData.currentStage;
    [Header("Events listener:On InGame Event is Rasied")]
    public VoidEventSO InGameEventTirgger;
    private static InGameEventManager _instance;
    public static InGameEventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<InGameEventManager>();

                if (_instance == null)
                {
                    GameObject InGameEventManagerObject = new GameObject("InGameEventManager");
                    _instance = InGameEventManagerObject.AddComponent<InGameEventManager>();
                    InGameEventManagerObject.transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
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

    private void OnEnable()
    {
        InGameEventTirgger.onEventRaised += HandleInGameEvent;
    }
    private void OnDisable()
    {
        InGameEventTirgger.onEventRaised -= HandleInGameEvent;
    }
    private void HandleInGameEvent()
    {
        //TODO: Handle InGame Event
        // Need a list to store the event
        Debug.Log($"InGameEventManager:Day:{currentDay} Stage:{currentStage}");
    }
}
