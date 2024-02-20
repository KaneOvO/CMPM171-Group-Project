using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/AfternoonEventSO")]
public class AfternoonEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this._id = "AfternoonEvent";
        this._startStage = Stage.Afternoon;
        this._eventType = InGameEventType.Daily;
    }
    public override void OnStart()
    {
        base.OnStart();
    }
    public override void OnEnd()
    {
        base.OnEnd();
    }
}

