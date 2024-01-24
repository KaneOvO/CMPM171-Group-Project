using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayStart : BasicEvent
{
    public DayStart() : base("DayStart", false) { }
    private float timer = 2f;
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        UIManager.Instance.testText.text = "A new day, wait for 10 seconds to quit this event";
    }

    public override void OnExit()
    {
        UIManager.Instance.testText.text = "the day start event is over";
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        UIManager.Instance.testText.text = $"A new day, wait for {timer.ToString("F2")} seconds to quit this event";
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
