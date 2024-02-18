using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noon : BasicStage
{
    public Noon() : base(Stage.Noon) { }

    public override void OnEnter()
    {
        timer = maxTime;
        saveData.currentStage = stage;
        stageManager.InGameEventCheck();
    }

    public override void OnExit()
    {
        timer = maxTime;
    }

    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, 2f);
    }
}
