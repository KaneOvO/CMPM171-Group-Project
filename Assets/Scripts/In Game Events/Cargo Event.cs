using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
[CreateAssetMenu(menuName = "InGameEvent/CargoEventSO")]
public class CargoEvent : InGameEvent
{

    public AssetReference cargoScene;
    public override void Initialization()
    {
        base.Initialization();
        this.day = 3;
        this.id = "CargoEvent";
        this.stage = Stage.Morning;
        this.eventType = InGameEventType.Special;
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
        Debug.Log("Cargo Event Quit.");
        stageManager.StageMoved();
    }
}

