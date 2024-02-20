using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/NoonEventSO")]
public class NoonEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this._id = "NoonEvent";
        this._startStage = Stage.Noon;
        this._eventType = InGameEventType.Daily;
    }
    public override void OnStart() { base.OnStart(); }
    public override void OnEnd() { base.OnEnd(); }
}

