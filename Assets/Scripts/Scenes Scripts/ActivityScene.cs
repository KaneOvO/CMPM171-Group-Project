using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ActivityScene : MonoBehaviour
{
    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadEventSO;
    [Header("Events Sender: Panel Called")]
    public StringParameterEventSO panelCalledEvent;
    [Header("Scene Script Object: Shop Scene")]
    public AssetReference shopScene;
    public void ShopButtonClicked()
    {
        loadEventSO.RaiseEvent(shopScene, true);
    }

    public void BackpackButtonClicked()
    {
        panelCalledEvent.RaiseEvent("BackpackPanel");
    }

    public void SettingButtonClicked()
    {
        panelCalledEvent.RaiseEvent("SettingsPanel");
    }
    public void OptionsButtonClicked()
    {
        panelCalledEvent.RaiseEvent("OptionsPanel");
    }
}
