using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.AddressableAssets;

public class CurrentManager : MonoBehaviour
{
    public Flowchart flowchart;
    public Localization localizationComponent;
    public int energy;
    public float money;
    public int moral;
    public int reputation;
    public int health;
    public bool isSick;
    public string currentLanguage;

    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadSceneEventSO;
    [Header("Scene Script Object: Shop Scene")]
    public AssetReference shopScene;
    [Header("Scene Script Object: Activity Scene")]
    public AssetReference activityScene;
    void Awake()
    {
        localizationComponent = FindObjectOfType<Localization>();
        flowchart = FindObjectOfType<Flowchart>();
    }
    public void InitialData()
    {
        SwitchLanguage();
        flowchart.SetStringVariable("Language", currentLanguage);

        energy = GameManager.Instance.saveData.playerState.energy;
        flowchart.SetIntegerVariable("Energy", energy);

        money = GameManager.Instance.saveData.playerState.money;
        flowchart.SetFloatVariable("Money", money);

        moral = GameManager.Instance.saveData.playerState.moral;
        flowchart.SetIntegerVariable("Moral", moral);

        reputation = GameManager.Instance.saveData.playerState.reputation;
        flowchart.SetIntegerVariable("Reputation", reputation);

        health = GameManager.Instance.saveData.playerState.health;
        flowchart.SetIntegerVariable("Health", health);

        isSick = GameManager.Instance.saveData.playerState.isSick;
        flowchart.SetBooleanVariable("IsSick", isSick);
    }


    private void SwitchLanguage()
    {
        currentLanguage = GameManager.Instance.saveData.currentLanguage switch
        {
            Language.English => "EN",
            Language.Chinese => "",
            Language.Japanese => "JP",
            _ => "EN",
        };
    }

    public void OnDestroy()
    {
        GameManager.Instance.saveData.playerState.energy = flowchart.GetIntegerVariable("Energy");
        GameManager.Instance.saveData.playerState.money = flowchart.GetFloatVariable("Money");
        GameManager.Instance.saveData.playerState.moral = flowchart.GetIntegerVariable("Moral");
        GameManager.Instance.saveData.playerState.reputation = flowchart.GetIntegerVariable("Reputation");
        GameManager.Instance.saveData.playerState.health = flowchart.GetIntegerVariable("Health");
        GameManager.Instance.saveData.playerState.isSick = flowchart.GetBooleanVariable("IsSick");
    }

    public void loadShopScene()
    {
#if UNITY_EDITOR
        Debug.Log("Load Scene");
#endif
        loadSceneEventSO.RaiseEvent(shopScene, true);
    }

    public void loadActivityScene()
    {
        loadSceneEventSO.RaiseEvent(activityScene, true);
    }
}
