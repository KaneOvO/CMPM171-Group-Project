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
    public int currentDay;
    public int currentStage;
    public string currentLanguage;

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

        currentDay = GameManager.Instance.saveData.currentDay;
        flowchart.SetIntegerVariable("CurrentDay", currentDay);

        JudgeStage();
        flowchart.SetIntegerVariable("CurrentStage", currentStage);
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

    public void JudgeStage()
    {
        currentStage = GameManager.Instance.saveData.currentStage switch
        {
            Stage.Morning => 1,
            Stage.Noon => 2,
            Stage.Afternoon => 3,
            Stage.Night => 4,
            _ => 1,
        };
    }

    public Stage JudgeStage2()
    {
        return currentStage switch
        {
            1 => Stage.Morning,
            2 => Stage.Noon,
            3 => Stage.Afternoon,
            4 => Stage.Night,
            _ => Stage.Morning,
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
        GameManager.Instance.saveData.currentDay = flowchart.GetIntegerVariable("CurrentDay");
        GameManager.Instance.saveData.currentStage = JudgeStage2();
    }

    public void StageMove()
    {
        StageManager.Instance.StageMoved();
    }
}
