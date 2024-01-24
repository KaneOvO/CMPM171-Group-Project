using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using UnityEngine.Networking;
using System.IO;

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
    public PlayerState playerState;
}
