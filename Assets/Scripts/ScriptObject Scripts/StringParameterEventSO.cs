using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/StringParameterEventSO")]
public class StringParameterEventSO : ScriptableObject
{
    public UnityAction<string> onEventRaised;
    public void RaiseEvent(string strings) { onEventRaised?.Invoke(strings); }
}