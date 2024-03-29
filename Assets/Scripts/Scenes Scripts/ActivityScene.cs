using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class ActivityScene : MonoBehaviour
{
    [Header("Events Sender: Panel Called")]
    public StringParameterEventSO panelCalledEvent;

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
    public void EnglishButtonClicked()
    {
        GameManager.Instance.playerConfig.currentLanguage = Language.English;
        UIManager.Instance.InvokeLanguageChangeEvents();
    }
    public void ChineseButtonClicked()
    {
        GameManager.Instance.playerConfig.currentLanguage = Language.Chinese;
        UIManager.Instance.InvokeLanguageChangeEvents();
    }
    public void CallLanguageChangeEvents()
    {
        UIManager.Instance.InvokeLanguageChangeEvents();
    }

    public void Start()
    {
        
    }
}
