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
    public List<BasicEvent> eventList = new List<BasicEvent>();
    private BasicEvent currentEvent;
    private int currentEventIndex = 0;
    public void Start()
    {
        EventWapper();
        LoadEvent(currentEventIndex);
    }
    private void EventWapper()
    {
        if (eventList.Count == 0)
        {
            eventList.Add(new DayStart());
            eventList.Add(new DayEnd());
            return;
        }
        eventList.RemoveAll(x => x.eventID == "DayStart" || x.eventID == "DayEnd");
        eventList.Insert(0, new DayStart());
        eventList.Add(new DayEnd());
    }

    private void LoadEvent(int currentEventIndex){
        currentEvent = eventList[currentEventIndex];
        currentEvent.OnEnter();
    }
    public void NextEvent(){
        currentEvent.OnExit();
        currentEventIndex++;
        LoadEvent(currentEventIndex);
    }

    private void Update(){
        currentEvent.OnUpdate();
    }
    private void FixedUpdate(){
        currentEvent.OnFixedUpdate();
    }
}
