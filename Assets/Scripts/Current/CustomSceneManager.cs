using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.AddressableAssets;
using System.Reflection;

public class CustomSceneManager : MonoBehaviour
{
    #region Events Sender: Load Scene
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadSceneEventSO;
    #endregion
    #region  Scene Script Object: Shop Scene
    [Header("Scene Script Object: Shop Scene")]
    public AssetReference shopSceneFirst;
    public AssetReference shopSceneStart;
    public AssetReference shopSceneEnd;
    #endregion
    #region  Scene Script Object: Temple Scene
    [Header("Scene Script Object: Temple Scene")]
    public AssetReference templeSceneFirst;
    public AssetReference templeSceneStart;
    public AssetReference templeSceneEnd;
    #endregion
    #region  Scene Script Object: Home Scene
    [Header("Scene Script Object: Home Scene")]
    public AssetReference homeSceneFirst;
    public AssetReference homeSceneStart;
    public AssetReference homeSceneEnd;
    #endregion
    #region  Scene Script Object: Labor Market Scene
    [Header("Scene Script Object: Labor Market Scene")]
    public AssetReference laborMarketSceneFirst;
    public AssetReference laborMarketSceneStart;
    public AssetReference laborMarketSceneEnd;
    #endregion
    #region  Scene Script Object: Wholesale Scene
    [Header("Scene Script Object: Wholesale Scene")]
    public AssetReference wholesaleSceneFirst;
    public AssetReference wholesaleSceneStart;
    public AssetReference wholesaleSceneEnd;
    #endregion
    #region  Scene Script Object: Activity Scene
    [Header("Scene Script Object: Activity Scene")]
    public AssetReference activityScene;
    #endregion
    #region  Scene Script Object: Cargo Event Scene
    [Header("Scene Script Object: Cargo Event Scene")]
    public AssetReference CargoEvent;
    #endregion

    #region Shop Scene
    public void loadShopSceneFirst()
    {
        loadSceneEventSO.RaiseEvent(shopSceneFirst, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadShopSceneStart()
    {
        loadSceneEventSO.RaiseEvent(shopSceneStart, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadShopSceneEnd()
    {
        loadSceneEventSO.RaiseEvent(shopSceneEnd, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }
    #endregion
    #region Temple Scene
    public void loadTempleSceneFirst()
    {
        loadSceneEventSO.RaiseEvent(templeSceneFirst, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadTempleSceneStart()
    {
        loadSceneEventSO.RaiseEvent(templeSceneStart, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadTempleSceneEnd()
    {
        loadSceneEventSO.RaiseEvent(templeSceneEnd, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }
    #endregion
    #region Home Scene
    public void loadHomeSceneFirst()
    {
        loadSceneEventSO.RaiseEvent(homeSceneFirst, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadHomeSceneStart()
    {
        loadSceneEventSO.RaiseEvent(homeSceneStart, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadHomeSceneEnd()
    {
        loadSceneEventSO.RaiseEvent(homeSceneEnd, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }
    #endregion
    #region Wholesale Scene
    public void loadWholesaleSceneFirst()
    {
        loadSceneEventSO.RaiseEvent(wholesaleSceneFirst, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadWholesaleSceneStart()
    {
        loadSceneEventSO.RaiseEvent(wholesaleSceneStart, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadWholesaleSceneEnd()
    {
        loadSceneEventSO.RaiseEvent(wholesaleSceneEnd, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }
    #endregion
    #region Labor Market Scene
    public void loadLaborMarketSceneFirst()
    {
        loadSceneEventSO.RaiseEvent(laborMarketSceneFirst, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadLaborMarketSceneStart()
    {
        loadSceneEventSO.RaiseEvent(laborMarketSceneStart, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }

    public void loadLaborMarketSceneEnd()
    {
        loadSceneEventSO.RaiseEvent(laborMarketSceneEnd, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }
    #endregion
    #region Activity Scene
    public void loadActivityScene()
    {
        loadSceneEventSO.RaiseEvent(activityScene, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }
    #endregion
    #region Cargo Event
    public void loadCargoEvent()
    {
        loadSceneEventSO.RaiseEvent(CargoEvent, true);
#if UNITY_EDITOR
        Debug.Log($"{MethodBase.GetCurrentMethod().Name} is called.");
#endif
    }
    #endregion
}
