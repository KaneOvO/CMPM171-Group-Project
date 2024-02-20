using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
[System.Serializable]
public class InGameEvent : ScriptableObject, IComparable<InGameEvent>
{
    public int currentDay => GameManager.Instance.saveData.currentDay;
    public InGameEventManager eventManager => InGameEventManager.Instance;
    public PlayerStateManager stateManager => PlayerStateManager.Instance;
    public StageManager stageManager => StageManager.Instance;
    public VoidEventSO onStageMoved;
    protected string _id;
    protected int _startDay;
    protected Stage _startStage;
    protected int _endDay;
    protected Stage _endStage;
    protected InGameEventType _eventType;
    public string id => _id;
    public Stage startStage => _startStage;
    public int startDay => _startDay;
    public Stage endStage => _endStage;
    public int endDay => _endDay;
    public InGameEventType eventType => _eventType;
    public bool isTriggered = false;
    public bool isTriggerable = true;
    public bool isEnded = false;
    public bool isEndAble = false;
    protected float timer = 0f;
    protected virtual void OnEnable()
    {
        Initialization();
        this.onStageMoved.onEventRaised += HandleStageMoved;
    }
    protected virtual void OnDisable()
    {
        this.onStageMoved.onEventRaised -= HandleStageMoved;
    }

    public virtual void HandleStageMoved()
    {
        return;
    }
    public virtual void Initialization()
    {
        this._id = "Default";
        this._startDay = 0;
        this._startStage = Stage.Default;
        this._endDay = 0;
        this._endStage = Stage.Default;
        this._eventType = InGameEventType.Default;
        this.isTriggered = false;
        this.isTriggerable = true;
        this.isEnded = false;
        this.isEndAble = false;
    }
    public virtual void OnStart()
    {
        isTriggered = true;
        isTriggerable = false;
        isEnded = false;
        isEndAble = false;
        timer = 0f;
    }
    public virtual void OnUpdate()
    {
        if (CheckEndCondition()) { OnEnd(); }
    }
    public virtual bool CheckEndCondition()
    {
        return isEndAble = true;
    }
    public virtual void OnEnd()
    {
        isEnded = true;
        timer = 0f;
        eventManager.currentEvents.Remove(this);
    }
    public int CompareTo(InGameEvent other)
    {
        int result = this.startDay.CompareTo(other.startDay);
        if (result == 0)
        {
            result = this.startStage.CompareTo(other.startStage);
            if (result == 0)
            {
                result = this.eventType.CompareTo(other.eventType);
                if (result == 0)
                    result = this.id.CompareTo(other.id);
            }
        }
        return result;
    }
}