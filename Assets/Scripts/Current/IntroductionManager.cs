using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fungus;

public class IntroductionManager : MonoBehaviour
{
    public Flowchart flowchart;
    public Localization localizationComponent;
    public string currentLanguage;
    // Start is called before the first frame update\
    void Awake()
    {
        localizationComponent = FindObjectOfType<Localization>();
        flowchart = FindObjectOfType<Flowchart>();

        
    }

    public void InitialData()
    {
        SwitchLanguage();
        flowchart.SetStringVariable("Language", currentLanguage);
        Debug.Log(flowchart.GetStringVariable("Language"));
        Debug.Log(GameManager.Instance.playerConfig.currentLanguage);
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


}
