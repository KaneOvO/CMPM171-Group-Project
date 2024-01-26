using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Night : BasicEvent
{
    public Night() : base("Night", false) { }
    private float timer = 2f;
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        PlayerStateManager.Instance.playerState.energy = 0;
        PlayerStateManager.Instance.playerState.isSick = false;
        UIManager.Instance.testText.text = "Now on Night.";
    }

    public override void OnExit()
    {
        UIManager.Instance.testText.text = "Good Night.";
    }
    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        UIManager.Instance.testText.text = $"Now on night, wait for {timer.ToString("F2")} seconds to quit this event";
        if (timer <= 0)
        {
            timer = 10f;
            EventManager.Instance.NextEvent();
        }
    }
    public override void OnFixedUpdate()
    {

    }
}
