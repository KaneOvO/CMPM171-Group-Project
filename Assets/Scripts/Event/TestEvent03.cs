using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent03 : BasicEvent
{
    public TestEvent03() : base("TestEvent03", false) { }
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        UIManager.Instance.testText.text = "Event 03 is started";
    }

    public override void OnExit()
    {
        UIManager.Instance.testText.text = "Event 03 is over";
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            EventManager.Instance.NextEvent();
        }
    }

    public override void OnFixedUpdate()
    {

    }
}