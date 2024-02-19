using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/NightEventSO")]
public class NightEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this.id = "NightEvent";
        this.stage = Stage.Night;
        this.eventType = InGameEventType.Daily;
    }
    public override void OnStart()
    {
        base.OnStart();
        stateManager.ClearEnergy();
    }
    public override void OnEnd()
    {
        base.OnEnd();
        eventManager.NewDay();
    }
}

