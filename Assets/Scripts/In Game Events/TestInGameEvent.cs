using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/TestInGameEventSO")]
public class TestInGameEvent : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this.id = "TestInGameEvent";
        this.stage = Stage.Default;
        this.eventType = InGameEventType.Daily;
    }
    public override void OnStart()
    {
        base.OnStart();
    }
    public override bool CheckEndCondition()
    {
        return isEndAble = Input.anyKeyDown;
    }
    public override void OnEnd()
    {
        base.OnEnd();
        isTriggered = false;
        Debug.Log("Event Quit.");
        stageManager.StageMoved();
    }
}

