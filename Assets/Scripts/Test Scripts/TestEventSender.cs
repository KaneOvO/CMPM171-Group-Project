using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TestEventSender : MonoBehaviour
{
    [Header("Events to send")]
    public UnityEvent senderEvnet;

    public void Awake()
    {
        senderEvnet?.Invoke();
    }
    public void PrintWorld()
    {
        Debug.Log($"Event raised! Form {GetType().Name}");
    }
}
