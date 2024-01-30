using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : BasicActivity
{
    public Night() : base("Night") { }
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        PlayerStateManager.Instance.ClearEnergy();
        PlayerStateManager.Instance.playerState.isSick = false;
        UIManager.Instance.testText.text = "Now on Night.";
    }

    public override void OnExit()
    {
        timer = maxTime;
        UIManager.Instance.testText.text = "Good Night.";
    }
    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, maxTime);
        UIManager.Instance.testText.text = $"Day:{GameManager.Instance.saveData.currentDay}\nNow on night, wait for {timer.ToString("F2")} seconds to quit this event";
        if (timer <= 0)
        {
            PlayerActivityManager.Instance.NewDay();
        }
    }
    public override void OnFixedUpdate()
    {

    }
}
