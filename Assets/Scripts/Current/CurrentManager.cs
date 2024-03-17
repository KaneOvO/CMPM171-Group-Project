using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;
using UnityEngine.AddressableAssets;

public class CurrentManager : MonoBehaviour
{
    public Flowchart flowchart;
    public Localization localizationComponent;
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
    private void OnEnable()
    {
        UIManager.Instance.onLanguageChange.AddListener(OnLanguageChanged);
    }

    public void OnLanguageChanged()
    {
        SwitchLanguage();
        flowchart.SetStringVariable("Language", currentLanguage);
    }
    public void InitialData()
    {
        SwitchLanguage();
        flowchart.SetStringVariable("Language", currentLanguage);

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
        Debug.Log("InitialData");
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
        currentLanguage = GameManager.Instance.playerConfig.currentLanguage switch
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
        UIManager.Instance.onLanguageChange.RemoveListener(OnLanguageChanged);
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
        eventInfo.elder1 = flowchart.GetBooleanVariable("ElderEvent1");
        eventInfo.interest1 = flowchart.GetBooleanVariable("InterestEvent1");
    }

    public void StageMove()
    {
        GameManager.Instance.saveData.currentStage = GameManager.Instance.saveData.currentStage switch
        {
            Stage.Morning => Stage.Afternoon,
            Stage.Afternoon => Stage.Night,
            Stage.Night => Stage.Morning,
            _ => Stage.Morning,
        };
        if (GameManager.Instance.saveData.currentStage == Stage.Morning)
        {
            GameManager.Instance.saveData.currentDay++;
            flowchart.SetIntegerVariable("CurrentDay", GameManager.Instance.saveData.currentDay);
        }
        JudgeStage();
        flowchart.SetIntegerVariable("CurrentStage", currentStage);

        JudgeSick();

    }

    public void JudgeSick()
    {
        if (flowchart.GetIntegerVariable("Health") <= 30)
        {
            int randomInt = UnityEngine.Random.Range(0, 99);
            if (randomInt < 50)
            {
                flowchart.SetBooleanVariable("IsSick", true);
            }
        }
    }

    public void UpdateInfo()
    {
        flowchart.SetBooleanVariable("teachPurchase", teachInfo.purchase);
        flowchart.SetBooleanVariable("teachVolunteer", teachInfo.volunteer);
        flowchart.SetBooleanVariable("teachSale", teachInfo.sale);
        flowchart.SetBooleanVariable("teachTakeBreak", teachInfo.takeBreak);
        flowchart.SetBooleanVariable("teachDayLabor", teachInfo.dayLabor);

        flowchart.SetBooleanVariable("CargoEvent", eventInfo.cargo);
        flowchart.SetBooleanVariable("ElderEvent1", eventInfo.elder1);
        flowchart.SetBooleanVariable("InterestEvent1", eventInfo.interest1);
    }

    public void SickeEffect()
    {
        flowchart.SetIntegerVariable("CurrentDay", currentDay++);
        flowchart.SetIntegerVariable("CurrentStage", 0);
        flowchart.SetIntegerVariable("Health", 50);
    }

    public void CargoEventEffect()
    {
        ItemManager.Instance.AddItemAmount("02", 600);
        ItemManager.Instance.AddItemAmount("03", 1000);
        ItemManager.Instance.AddItemAmount("04", 500);
    }

    
}
