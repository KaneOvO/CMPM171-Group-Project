using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class BasicStage
{
    public readonly Stage stage;
    public float timer;
    public float maxTime;
    protected GameManager gameManager => GameManager.Instance;
    protected SaveData saveData => GameManager.Instance.saveData;
    protected StageManager stageManager => StageManager.Instance;
    protected PlayerStateManager stateManager => PlayerStateManager.Instance;
    protected InGameEventManager eventManager => InGameEventManager.Instance;
    public BasicStage(Stage stage, float maxTime = 2f)
    {
        this.stage = stage;
        this.maxTime = maxTime;
    }
    public abstract void OnEnter();
    public abstract void OnExit();
    public abstract void OnUpdate();
}
