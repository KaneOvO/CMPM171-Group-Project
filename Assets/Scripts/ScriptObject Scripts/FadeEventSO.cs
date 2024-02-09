using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/FadeEventSO")]
public class FadeEventSO : ScriptableObject
{
    public UnityAction<Color, float> onEventRised;
    public void FadeIn(float duration) { RaiseEvent(Color.black, duration); }

    public void FadeOut(float duration) { RaiseEvent(Color.clear, duration); }

    public void RaiseEvent(Color color, float duration)
    {
        onEventRised?.Invoke(color, duration);
    }
}
