using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "Events/SceneLoadEventSO")]
public class SceneLoadEventSO : ScriptableObject
{
    public UnityAction<AssetReference, bool> onEventRaised;
    public void RaiseEvent(AssetReference scene, bool fade)
    {
        onEventRaised?.Invoke(scene, fade);
    }
}