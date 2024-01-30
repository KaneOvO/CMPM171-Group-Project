using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAction : BasicActivity
{
    public PickAction() : base("PickAction") { }
    public PlayerState initialPlayerStates => PlayerStateManager.Instance.initialPlayerStates;
    public PlayerState playerState => PlayerStateManager.Instance.playerState;
    public override void Buff()
    {

    }

    public override void OnEnter()
    {
        timer = maxTime;
        UIManager.Instance.testText.text = $"Day:{GameManager.Instance.saveData.currentDay}\nCurrent Energy:{PlayerStateManager.Instance.playerState.energy}\nNow on PickAction.\nPress Space to continue.";
    }

    public override void OnExit()
    {
        timer = maxTime;
    }

    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, maxTime);
        if (Input.GetKeyUp(KeyCode.Space))
        {
            Debug.Log("Space is pressed");
            PlayerActivityManager.Instance.NextActivity();
        }
    }

    public override void OnFixedUpdate()
    {

    }
}