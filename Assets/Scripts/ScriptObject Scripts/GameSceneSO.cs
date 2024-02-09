using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

[CreateAssetMenu(menuName = "GameScene/SceneLoadEventSO")]
public class GameSceneSO : ScriptableObject
{
    public SceneType sceneType;
    public AssetReference sceneReference;
}