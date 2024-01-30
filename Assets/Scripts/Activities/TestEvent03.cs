using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent03 : BasicActivity
{
    public TestEvent03() : base("TestEvent03") { }
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
            PlayerActivityManager.Instance.NextActivity();
        }
    }

    public override void OnFixedUpdate()
    {

    }
}