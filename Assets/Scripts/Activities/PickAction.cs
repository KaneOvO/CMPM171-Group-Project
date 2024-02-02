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
        UIManager.Instance.testText.text = $"Day:{GameManager.Instance.saveData.currentDay}\nCurrent Energy:{PlayerStateManager.Instance.playerState.energy}\nNow on {id}.\nPress Space or Click to continue.";
    }

    public override void OnExit()
    {
        timer = maxTime;
    }

    public override void OnUpdate()
    {
        timer = Mathf.Clamp(timer - Time.deltaTime, 0, maxTime);
        if (Input.GetKeyUp(KeyCode.Space)||Input.GetMouseButtonUp(0))
        {
            string useringPut = Input.GetKeyUp(KeyCode.Space)? "Space":"Mouse";
            Debug.Log($"{useringPut} is pressed");
            if(playerState.energy <= 0){
                PlayerActivityManager.Instance.energyEmpty.Invoke();
                return;
            }
            PlayerActivityManager.Instance.NextActivity();
        }
    }

    public override void OnFixedUpdate()
    {

    }
}
