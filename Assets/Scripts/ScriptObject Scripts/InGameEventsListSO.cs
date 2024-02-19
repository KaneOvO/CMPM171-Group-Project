using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "InGameEvent/GameEventsListSO")]
public class InGameEventsListSO : ScriptableObject
{
    public List<InGameEvent> eventList;
    protected void OnEnable() { SortEventList(); }
    public InGameEvent GetEvent(string id)
    {
        return eventList?.Find(x => x.id == id);
    }
    [ContextMenu("Sort Event List")]
    public void SortEventList()
    {
        eventList?.Sort();
#if UNITY_EDITOR
        Debug.Log("Event List is sorted");
#endif
    }

    [ContextMenu("Events IDs")]
    public void GetEvents()
    {
        foreach (InGameEvent e in eventList)
        {
            Debug.Log(e.id);
        }
    }
    public List<InGameEvent> GetAllTriggeredEvents()
    {
        List<InGameEvent> triggeredEvents = new List<InGameEvent>();
        foreach (InGameEvent e in eventList)
        {
            if (e.isTriggered)
            {
                triggeredEvents.Add(e);
            }
        }
        return triggeredEvents;
    }
    public List<InGameEvent> GetAllOngoingEvents()
    {
        List<InGameEvent> ongoingEvents = new List<InGameEvent>();
        foreach (InGameEvent e in eventList)
        {
            if (e.isTriggered && !e.isEnded)
            {
                ongoingEvents.Add(e);
            }
        }
        return ongoingEvents;
    }

    public List<InGameEvent> GetAllEndedEvents()
    {
        List<InGameEvent> endedEvents = new List<InGameEvent>();
        foreach (InGameEvent e in eventList)
        {
            if (e.isEnded)
            {
                endedEvents.Add(e);
            }
        }
        return endedEvents;
    }

    public List<InGameEvent> GetAllUnTriggeredEvents()
    {
        List<InGameEvent> unTriggeredEvents = new List<InGameEvent>();
        foreach (InGameEvent e in eventList)
        {
            if (!e.isTriggered)
            {
                unTriggeredEvents.Add(e);
            }
        }
        return unTriggeredEvents;
    }
    public List<InGameEvent> GetAllUnEndedEvents()
    {
        List<InGameEvent> unEndedEvents = new List<InGameEvent>();
        foreach (InGameEvent e in eventList)
        {
            if (!e.isEnded)
            {
                unEndedEvents.Add(e);
            }
        }
        return unEndedEvents;
    }
}