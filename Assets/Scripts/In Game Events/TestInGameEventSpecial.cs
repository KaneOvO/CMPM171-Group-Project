using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "InGameEvent/TestInGameEventSpecialSO")]
public class TestInGameEventSpecial : InGameEvent
{
    public override void Initialization()
    {
        base.Initialization();
        this._id = "TestInGameEventSpecial";
        this._startDay = 3;
        this._startStage = Stage.Morning;
        this._endDay = 3;
        this._endStage = Stage.Morning;
        this._eventType = InGameEventType.Special;
    }
    public override void OnStart()
    {
        base.OnStart();
        Debug.Log($"Event: {this.id} is Started.");
        stateManager.playerState.health -= 10;
    }
    public override bool CheckEndCondition()
    {
        return isEndAble = isTriggered && currentDay >= endDay && stageManager.currentStage >= endStage;
    }
    public override void HandleStageMoved()
    {
        if (CheckEndCondition()) OnEnd();
    }
    public override void OnUpdate()
    {
        return;
    }
    public override void OnEnd()
    {
        base.OnEnd();
        Debug.Log($"Event: {this.id} is Quit.");
        stateManager.playerState.health += 10;
    }
}

