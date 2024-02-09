using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/VoidEventSO")]
public class VoidEventSO : ScriptableObject
{
    public UnityAction onEventRaised;
    public void RaiseEvent()
    {
        onEventRaised?.Invoke();
    }
}
