using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class InitializedScene : MonoBehaviour
{
    public AssetReference onGoingScene;
    private void Awake()
    {
        Addressables.LoadSceneAsync(onGoingScene);
    }
}
