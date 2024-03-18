using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Fungus;
using JetBrains.Annotations;

public class AudioManager : MonoBehaviour
{
    public Slider volumeSlider;
    public Flowchart flowchart;

    void Awake()
    {
        flowchart = FindObjectOfType<Flowchart>();
    }

    void Start()
    {
        if (PlayerPrefs.HasKey("PlayerConfig"))
        {
            volumeSlider.value = GameManager.Instance.playerConfig.volume;

            FungusManager.Instance.MusicManager.audioSourceMusic.volume = volumeSlider.value;

            SetVolume();
        }
    }

    public void SetVolume()
    {
        FungusManager.Instance.MusicManager.SetAudioVolume(volumeSlider.value, 1f, null);
        GameManager.Instance.playerConfig.volume = volumeSlider.value;
    }

    public void SavePlayerConfig()
    {
        Invoke(nameof(Save), 0.5f);

    }

    void Save()
    {
        string playerConfigJson = JsonUtility.ToJson(GameManager.Instance.playerConfig);
        PlayerPrefs.SetString("PlayerConfig", playerConfigJson);
        PlayerPrefs.Save();
    }

    public void Savedata()
    {
        GameManager.Instance.Save(true);
    }

    public void ResetGame()
    {
        flowchart.SetIntegerVariable("CurrentDay", 1);
        flowchart.SetIntegerVariable("CurrentStage", 0);
        flowchart.SetFloatVariable("Money", 5000);
        flowchart.SetIntegerVariable("Moral", 50);
        flowchart.SetIntegerVariable("Reputation", 0);
        flowchart.SetIntegerVariable("Health", 80);
        flowchart.SetBooleanVariable("IsSick", false);
        flowchart.SetBooleanVariable("teachPurchase", false);
        flowchart.SetBooleanVariable("teachVolunteer", false);
        flowchart.SetBooleanVariable("teachSale", false);
        flowchart.SetBooleanVariable("teachTakeBreak", false);
        flowchart.SetBooleanVariable("teachDayLabor", false);
        flowchart.SetBooleanVariable("CargoEvent", false);
        flowchart.SetBooleanVariable("ElderEvent1", false);
        flowchart.SetBooleanVariable("InterestEvent1", false);
    }

    public void LoadButton()
    {
        flowchart.SetIntegerVariable("CurrentDay", GameManager.Instance.saveData.currentDay);
        flowchart.SetIntegerVariable("CurrentStage", (int)GameManager.Instance.saveData.currentStage);
        string currentLanguage = GameManager.Instance.playerConfig.currentLanguage switch
        {
            Language.English => "EN",
            Language.Chinese => "",
            Language.Japanese => "JP",
            _ => "EN",
        };
        flowchart.SetStringVariable("Language", currentLanguage);

        flowchart.SetFloatVariable("Money", GameManager.Instance.saveData.playerState.money);
        flowchart.SetIntegerVariable("Moral", GameManager.Instance.saveData.playerState.moral);
        flowchart.SetIntegerVariable("Reputation", GameManager.Instance.saveData.playerState.reputation);
        flowchart.SetIntegerVariable("Health", GameManager.Instance.saveData.playerState.health);
        flowchart.SetBooleanVariable("IsSick", GameManager.Instance.saveData.playerState.isSick);


        flowchart.SetBooleanVariable("teachPurchase", GameManager.Instance.saveData.teachInfo.purchase);
        flowchart.SetBooleanVariable("teachVolunteer", GameManager.Instance.saveData.teachInfo.volunteer);
        flowchart.SetBooleanVariable("teachSale", GameManager.Instance.saveData.teachInfo.sale);
        flowchart.SetBooleanVariable("teachTakeBreak", GameManager.Instance.saveData.teachInfo.takeBreak);
        flowchart.SetBooleanVariable("teachDayLabor", GameManager.Instance.saveData.teachInfo.dayLabor);
        flowchart.SetBooleanVariable("CargoEvent", GameManager.Instance.saveData.eventInfo.cargo);
        flowchart.SetBooleanVariable("ElderEvent1", GameManager.Instance.saveData.eventInfo.elder1);
        flowchart.SetBooleanVariable("InterestEvent1", GameManager.Instance.saveData.eventInfo.interest1);

    }



}
