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
    }
    private void Update()
    {
        CheckSick();
    }
    public PlayerState playerState => GameManager.Instance.saveData.playerState;
    public PlayerState initialPlayerStates => GameManager.Instance.inGameData.initialDatas.playerState;
    [ContextMenu("Check Health")]
    public bool CheckSick()
    {
        return playerState.isSick = playerState.health * 2 < initialPlayerStates.health;
    }
}
