using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAction : BasicEvent
{
    public PickAction() : base("PickAction", false) { }
    private float timer = 2f;
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        Debug.Log("Now enter PickAction");
    }

    public override void OnExit()
    {
        Debug.Log("Now exit PickAction");
        PlayerStateManager.Instance.playerState.energy = Mathf.Clamp(PlayerStateManager.Instance.playerState.energy - 1, 0, 3);
    }

    public override void OnUpdate()
    {
        timer -= Time.deltaTime;
        UIManager.Instance.testText.text = $"Now on Pick action, wait for {timer.ToString("F2")} seconds to quit this event";
        if (timer <= 0)
        {
            timer = 0f;
            UIManager.Instance.testText.text = "Press Space to continue";
            if (Input.GetKeyDown(KeyCode.Space))
            {
                EventManager.Instance.NextEvent();
            }
        }
    }

    public override void OnFixedUpdate()
    {

    }
}
