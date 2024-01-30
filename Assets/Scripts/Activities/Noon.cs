using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noon : BasicActivity
{
    public Noon() : base("Noon") { }
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
      timer = maxTime;
    }

    public override void OnExit()
    {
        timer = maxTime;
        PlayerStateManager.Instance.EnergyChange(-1);
    }

    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, 2f);
        UIManager.Instance.testText.text = $"Day:{GameManager.Instance.saveData.currentDay}\nNow on Noon. Wait {timer.ToString("F2")} seconds to continue.";
        if (timer <= 0) PlayerActivityManager.Instance.NextActivity();
    }

    public override void OnFixedUpdate()
    {

    }
}