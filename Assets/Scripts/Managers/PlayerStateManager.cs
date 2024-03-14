using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Networking;
using System.IO;
using System;
using UnityEditor;
[AddComponentMenu("Managers/PlayerStateManager")]
public class PlayerStateManager : MonoBehaviour
{
    [Header("Events listener:On Player Activity is Done")]
    public VoidEventSO ActivityDoneEvent;
    private static PlayerStateManager _instance;
    public static PlayerStateManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerStateManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
    }

    public PlayerState playerState => GameManager.Instance.saveData.playerState;
    public PlayerState initialPlayerStates => GameManager.Instance.inGameData.initialDatas.playerState;
    public int sick => GameManager.Instance.inGameData.initialDatas.sick;

    private void OnEnable()
    {
        ActivityDoneEvent.onEventRaised += HandleActivityDone;
    }

    private void OnDisable()
    {
        ActivityDoneEvent.onEventRaised -= HandleActivityDone;
    }

    private void HandleActivityDone()
    {
        CheckSick();
    }

    [ContextMenu("Check Health")]
    public bool CheckSick()
    {
        return playerState.isSick = playerState.health < sick;
    }
}
