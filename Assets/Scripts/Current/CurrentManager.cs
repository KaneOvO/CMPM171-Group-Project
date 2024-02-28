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
    public TeachInfo teachInfo => GameManager.Instance.saveData.teachInfo;
    public EventInfo eventInfo => GameManager.Instance.saveData.eventInfo;

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

        UpdateMoney();

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

        UpdateInfo();

        JudgeStage();
        flowchart.SetIntegerVariable("CurrentStage", currentStage);
    }

    public void UpdateMoney()
    {
        money = GameManager.Instance.saveData.playerState.money;
        flowchart.SetFloatVariable("Money", money);
    }

    public void DebugTest()
    {
        Debug.Log("DebugTest");
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
        teachInfo.purchase = flowchart.GetBooleanVariable("teachPurchase");
        teachInfo.sale = flowchart.GetBooleanVariable("teachSale");
        teachInfo.takeBreak = flowchart.GetBooleanVariable("teachTakeBreak");
        teachInfo.volunteer = flowchart.GetBooleanVariable("teachVolunteer");
        teachInfo.dayLabor = flowchart.GetBooleanVariable("teachDayLabor");
        eventInfo.cargo = flowchart.GetBooleanVariable("CargoEvent");
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

    public void UpdateInfo()
    {
        flowchart.SetBooleanVariable("teachPurchase", teachInfo.purchase);
        flowchart.SetBooleanVariable("teachVolunteer", teachInfo.volunteer);
        flowchart.SetBooleanVariable("teachSale", teachInfo.sale);
        flowchart.SetBooleanVariable("teachTakeBreak", teachInfo.takeBreak);
        flowchart.SetBooleanVariable("teachDayLabor", teachInfo.dayLabor);

        flowchart.SetBooleanVariable("CargoEvent", eventInfo.cargo);
    }
}
