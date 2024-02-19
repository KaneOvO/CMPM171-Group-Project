using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;
[System.Serializable]
public class InGameEvent : ScriptableObject, IComparable<InGameEvent>
{
    public InGameEventManager eventManager => InGameEventManager.Instance;
    public PlayerStateManager stateManager => PlayerStateManager.Instance;
    public StageManager stageManager => StageManager.Instance;
    public string id { get; protected set; }
    public Stage stage { get; protected set; }
    public int day { get; protected set; }
    public InGameEventType eventType { get; protected set; }
    public bool isTriggered;
    public bool isEnded;
    public bool isEndAble;
    protected float timer = 0f;
    public virtual void OnStart() { isTriggered = true; isEnded = false; isEndAble = false; timer = 0f; }
    public virtual bool CheckEndCondition()
    {
        return isEndAble = true;
    }
    public virtual void OnEnd() { isEnded = true; timer = 0f; eventManager.currentEvents.Remove(this); }
    public virtual void OnUpdate()
    {
        timer = Mathf.Clamp(timer + Time.deltaTime, 0f, 2f);
        if (CheckEndCondition()) { OnEnd(); }
    }
    protected virtual void OnEnable() { Initialization(); }
    public virtual void Initialization()
    {
        this.id = "Default";
        this.stage = Stage.Default;
        this.day = 0;
        this.eventType = InGameEventType.Default;
        this.isTriggered = false;
        this.isEnded = false;
        this.isEndAble = false;
    }
    public int CompareTo(InGameEvent other)
    {
        int result = this.day.CompareTo(other.day);
        if (result == 0)
        {
            result = this.stage.CompareTo(other.stage);
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