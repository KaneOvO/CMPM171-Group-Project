using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : BasicStage
{
    public Night() : base(Stage.Night) { }

    public override void OnEnter()
    {
        timer = maxTime;
        saveData.currentStage = stage;
        stateManager.ClearEnergy();
    }

    public override void OnExit()
    {
        timer = maxTime;
    }
    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, maxTime);
        if (timer <= 0 && Input.anyKeyDown)
        {
            stageManager.NewDay();
        }
    }
}
