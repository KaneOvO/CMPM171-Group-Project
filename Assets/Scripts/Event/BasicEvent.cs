using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BasicEvent
{
    public readonly string eventID;
    public readonly bool skippable;
    public BasicEvent(string id, bool isSkippable)
    {
        eventID = id;
        skippable = isSkippable;
    }
    public virtual void Buff() { }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
}
