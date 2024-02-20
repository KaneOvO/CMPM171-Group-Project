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

    [Header("Events List.")]
    public InGameEventsListSO inGameEventsListSO;
    [Header("Event Listener:On Stage Moved.")]
    public VoidEventSO onStageMoved;
    public List<InGameEvent> eventList => inGameEventsListSO.eventList;
    public List<InGameEvent> currentEvents = new List<InGameEvent>();
    public List<InGameEvent> newEvents = new List<InGameEvent>();
    public bool runUpdate = true;
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
        onStageMoved.onEventRaised += HandleStageMoved;

    }
    private void OnDisable()
    {
        onStageMoved.onEventRaised -= HandleStageMoved;
    }
    private void HandleStageMoved()
    {
        GetNewEvents();
        currentEvents.AddRange(newEvents);
        currentEvents.Sort();
        for (int i = 0; i < currentEvents.Count; i++)
        {
            currentEvents[i].OnStart();
        }
    }
    [ContextMenu("Get New Events")]
    public List<InGameEvent> GetNewEvents()
    {
        return newEvents = eventList.FindAll(x => CheckInGameEventValid(x));
    }


    public void LoadInGameEvent(string id)
    {
        InGameEvent inGameEvent = eventList.Find(x => x.id == id);
        if (inGameEvent != null && CheckInGameEventValid(inGameEvent))
        {
            inGameEvent.OnStart();
        }
    }

    public void LoadInGameEvent(InGameEvent inGameEvent)
    {
        if (CheckInGameEventValid(inGameEvent))
        {
            inGameEvent.OnStart();
        }
    }

    public void RemoveAllCurrentInGameEvents()
    {
        foreach (InGameEvent inGameEvent in currentEvents)
        {
            inGameEvent.OnEnd();
        }
        currentEvents.Clear();
    }

    private bool CheckInGameEventValid(InGameEvent inGameEvent)
    {
        return (inGameEvent.startDay == currentDay || inGameEvent.eventType == InGameEventType.Daily) && (inGameEvent.startStage == currentStage || inGameEvent.startStage == Stage.Default) && !inGameEvent.isTriggered && inGameEvent.isTriggerable;
    }

    private void Update()
    {
        if (!runUpdate) return;
        if (currentEvents == null) { return; }
        else
        {
            for (int i = 0; i < currentEvents.Count; i++)
            {
                if (!currentEvents[i].isEnded)
                {
                    currentEvents[i].OnUpdate();
                }
            }
        }
    }
}
