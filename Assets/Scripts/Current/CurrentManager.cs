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
        currentStage = (int)GameManager.Instance.saveData.currentStage;
    }

    public void JudgeStage2()
    {


    }

    public void OnDisable()
    {
        GameManager.Instance.saveData.playerState.energy = flowchart.GetIntegerVariable("Energy");
        GameManager.Instance.saveData.playerState.money = flowchart.GetFloatVariable("Money");
        GameManager.Instance.saveData.playerState.moral = flowchart.GetIntegerVariable("Moral");
        GameManager.Instance.saveData.playerState.reputation = flowchart.GetIntegerVariable("Reputation");
        GameManager.Instance.saveData.playerState.health = flowchart.GetIntegerVariable("Health");
        GameManager.Instance.saveData.playerState.isSick = flowchart.GetBooleanVariable("IsSick");
    }

    public void StageMove()
    {
        GameManager.Instance.saveData.currentStage = GameManager.Instance.saveData.currentStage switch
        {
            Stage.Morning => Stage.Noon,
            Stage.Noon => Stage.Afternoon,
            Stage.Afternoon => Stage.Night,
            Stage.Night => Stage.Morning,
            _ => Stage.Morning,
        };
        if (GameManager.Instance.saveData.currentStage == Stage.Morning)
        {
            GameManager.Instance.saveData.currentDay++;
            flowchart.SetIntegerVariable("CurrentDay", currentDay);
        }
        JudgeStage();
        flowchart.SetIntegerVariable("CurrentStage", currentStage);
    }
}
