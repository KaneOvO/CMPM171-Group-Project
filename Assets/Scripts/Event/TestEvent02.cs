using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEvent02 : BasicEvent
{
    public TestEvent02() : base("TestEvent02", false) { }
    private Coroutine testcoroutine;
    public override void Buff()
    {
        
    }

    public override void OnEnter()
    {
        UIManager.Instance.testText.text = "Event 02 is started";
    }

    public override void OnExit()
    {
        UIManager.Instance.testText.text = "Event 02 is over";
    }

    IEnumerator WaitToQuit()
    {
        float timer = 2f;
        
        while (timer > 0)
        {
            UIManager.Instance.testText.text = $"Wait for {timer.ToString("F2")} seconds to quit this event";
            yield return null;
            timer -= Time.deltaTime;
        }
        EventManager.Instance.NextEvent();
    }

    public override void OnUpdate()
    {
        if (Input.GetKeyUp(KeyCode.Space) && testcoroutine == null)
        {
            testcoroutine = EventManager.Instance.StartCoroutine(WaitToQuit());
        }
    }

    public override void OnFixedUpdate()
    {

    }
}