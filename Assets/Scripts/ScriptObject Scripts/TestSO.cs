using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/testSO")]
public class TestSO : ScriptableObject
{
    public UnityAction onEventRaised;
    public void RaiseEvent()
    {
        onEventRaised?.Invoke();
    }
}
