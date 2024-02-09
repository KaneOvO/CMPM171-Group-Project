using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class TestEventListener : MonoBehaviour
{
    [Header("Events to listen to")]
    public TestSO testSO;
    private void OnEnable()
    {
        // testSO.onEventRaised.AddListener(PrintWorld);
        testSO.onEventRaised += PrintWorld;
    }
    private void OnDisable()
    {
        // testSO.onEventRaised.RemoveListener(PrintWorld);
        testSO.onEventRaised -= PrintWorld;
    }
    private void PrintWorld()
    {
        Debug.Log($"Event raised! Form {GetType().Name}");
    }
}
