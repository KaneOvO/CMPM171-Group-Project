using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayStart : BasicEvent
{
    public DayStart() : base("DayStart", false) { }
    private float timer = 0;
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        Debug.Log("Now enter DayStart");
    }

    public override void OnExit()
    {
        Debug.Log("Now exit DayStart");
    }

    public override void OnUpdate()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer > 10)
        {
            EventManager.Instance.NextEvent();
        }
    }

    public override void OnFixedUpdate()
    {

    }
}
