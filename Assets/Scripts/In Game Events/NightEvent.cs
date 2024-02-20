using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/NightEventSO")]
public class NightEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this._id = "NightEvent";
        this._startStage = Stage.Night;
        this._eventType = InGameEventType.Daily;
    }
    public override void OnStart()
    {
        base.OnStart();
        stateManager.ClearEnergy();
    }
    public override void OnEnd()
    {
        base.OnEnd();
        NewDay();
    }
    public void NewDay()
    {
        foreach (InGameEvent ige in eventManager.eventList)
        {
            if (ige.eventType == InGameEventType.Daily)
            {
                ige.Initialization();
            }
        }
        GameManager.Instance.saveData.currentDay++;
    }
}

