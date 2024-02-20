using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/MorningEventSO")]
public class MorningEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this._id = "MorningEvent";
        this._startStage = Stage.Morning;
        this._eventType = InGameEventType.Daily;
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

