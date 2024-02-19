using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/NoonEventSO")]
public class NoonEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this.id = "NoonEvent";
        this.stage = Stage.Noon;
        this.eventType = InGameEventType.Daily;
    }
    public override void OnStart() { base.OnStart(); }
    public override void OnEnd() { base.OnEnd(); }
}

