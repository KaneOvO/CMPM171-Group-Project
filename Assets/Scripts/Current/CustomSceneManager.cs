using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.AddressableAssets;

public class CustomSceneManager : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadSceneEventSO;
    [Header("Scene Script Object: Shop Scene")]
    public AssetReference shopSceneFirst;
    public AssetReference shopSceneStart;
    public AssetReference shopSceneEnd;
    [Header("Scene Script Object: Activity Scene")]
    public AssetReference activityScene;

    [Header("Scene Script Object: Cargo Event Scene")]
    public AssetReference CargoEvent;
    

    public void loadShopSceneFirst()
    {
        loadSceneEventSO.RaiseEvent(shopSceneFirst, true);
    }

    public void loadShopSceneStart()
    {
        loadSceneEventSO.RaiseEvent(shopSceneStart, true);
    }

    public void loadShopSceneEnd()
    {
        loadSceneEventSO.RaiseEvent(shopSceneEnd, true);
    }

    public void loadActivityScene()
    {
        loadSceneEventSO.RaiseEvent(activityScene, true);
    }

    public void loadCargoEvent()
    {
        loadSceneEventSO.RaiseEvent(CargoEvent, true);
    }
}
