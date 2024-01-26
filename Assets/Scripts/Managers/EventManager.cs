using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    private static EventManager _instance;
    public static EventManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<EventManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    [SerializeField]
    public List<BasicEvent> eventList = new List<BasicEvent>();
    private readonly List<BasicEvent> initializedEventList = new List<BasicEvent>()
    {
        new Morning(),
        new PickAction(),
        new Noon(),
        new PickAction(),
        new Afternoon(),
        new PickAction(),
        new Night()
    };
    private BasicEvent currentEvent;
    private int currentEventIndex = 0;
    public void Start()
    {
        InitializedEventList();
        LoadEvent(currentEventIndex);
    }
    private void InitializedEventList()
    {
        eventList.Clear();
        eventList.AddRange(initializedEventList);
    }

    private void LoadEvent(int currentEventIndex)
    {
        currentEventIndex = Mathf.Clamp(currentEventIndex, 0, eventList.Count - 1);
        currentEvent = eventList[currentEventIndex];
        currentEvent.OnEnter();
    }
    public void NextEvent()
    {
        QuitEvent();
        currentEventIndex++;
        LoadEvent(currentEventIndex);
    }

    public void QuitEvent()
    {
        currentEvent.OnExit();
    }

    public void LoadEvent(BasicEvent basicEvent)
    {
        currentEvent = eventList.Find(x => x.id == basicEvent.id);
        if (currentEvent == null) Debug.Log($"Current event {basicEvent.id} is not found");
        currentEventIndex = currentEvent == null ? eventList.Count - 1 : eventList.FindIndex(x => x.id == basicEvent.id);
        LoadEvent(currentEventIndex);
    }

    private void Update()
    {
        currentEvent.OnUpdate();
    }
    private void FixedUpdate()
    {
        currentEvent.OnFixedUpdate();
    }
}
