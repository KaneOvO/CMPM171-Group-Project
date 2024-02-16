using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


[CreateAssetMenu(menuName = "Events/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<GameSceneSO, bool> onEventRaised;
    public void RaiseEvent(GameSceneSO scene, bool fade)
    {
        onEventRaised?.Invoke(scene, fade);
    }
}