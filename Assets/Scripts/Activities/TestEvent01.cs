using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class TestEvent01 : BasicActivity
{
    public TestEvent01() : base("TestEvent01") { }
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
       
    }

    public override void OnExit()
    {
        UIManager.Instance.testText.text = "Event 01 is over";
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
