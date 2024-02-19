using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/MorningEventSO")]
public class MorningEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this.id = "MorningEvent";
        this.stage = Stage.Morning;
        this.eventType = InGameEventType.Daily;
    }
    public override void OnStart()
    {
        base.OnStart();
        stateManager.ResetEnergy();
    }
    public override void OnEnd()
    {
        base.OnEnd();
    }
}

