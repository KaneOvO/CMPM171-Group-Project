using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morning : BasicStage
{
    public Morning() : base(Stage.Morning) { }
    public override void OnEnter()
    {
        timer = maxTime;
        saveData.currentStage = stage;
        stateManager.ResetEnergy();
        stageManager.InGameEventCheck();
    }

    public override void OnExit()
    {
        timer = maxTime;
    }

    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, maxTime);
    }
}
