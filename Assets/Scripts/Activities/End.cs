using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : BasicActivity
{
    public End() : base("End") { }
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        timer = maxTime;
        UIManager.Instance.testText.text = $"Now on day:{GameManager.Instance.saveData.currentDay}\nGame is over.";
    }

    public override void OnExit()
    {
        timer = maxTime;
    }

    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, 2f);
    }

    public override void OnFixedUpdate()
    {

    }
}
