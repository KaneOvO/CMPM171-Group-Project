using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Morning : BasicActivity
{
    public Morning() : base("Morning") { }
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
        UIManager.Instance.testText.text = "the day start event is over";
        PlayerStateManager.Instance.ResetEnergy();
    }

    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, maxTime);
        UIManager.Instance.testText.text = $"Day:{GameManager.Instance.saveData.currentDay}\nNow on {id}.\nWait for {timer.ToString("F2")} seconds to continue.";
        if (timer <= 0)
        {
            PlayerActivityManager.Instance.NextActivity();
        }
    }

    public override void OnFixedUpdate()
    {

    }
}
