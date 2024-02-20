using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/TestInGameEventDailySO")]
public class TestInGameEventDaily : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this._id = "TestInGameEventDaily";
        this._startStage = Stage.Default;
        this._eventType = InGameEventType.Daily;
    }
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log($"Event: {this.id} is Started.");
    }
    public override bool CheckEndCondition()
    {
        return isEndAble = Input.GetKeyDown(KeyCode.Space);
    }
    public override void OnEnd()
    {
        base.OnEnd();
        isTriggered = false;
        isTriggerable = true;
        Debug.Log($"Event: {this.id} is Quit.");
        stageManager.StageMoved();
    }
}

