using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmptyStage : BasicStage
{
    public EmptyStage() : base(Stage.Default) { }

    public override void OnEnter()
    {
        timer = maxTime;
        saveData.currentStage = stage;
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
