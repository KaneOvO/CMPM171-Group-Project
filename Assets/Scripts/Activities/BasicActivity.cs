using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BasicActivity
{
    public readonly string id;
    public float timer;
    public float maxTime;
    public BasicActivity(string id, float maxTime = 2f)
    {
        this.id = id;
        this.maxTime = maxTime;
    }
    public virtual void Buff() { }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
    public abstract void OnFixedUpdate();
}
