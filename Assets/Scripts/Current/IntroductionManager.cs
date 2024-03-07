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

        currentLanguage = GameManager.Instance.saveData.currentLanguage switch
        {
            Language.English => "EN",
            Language.Chinese => "",
            Language.Japanese => "JP",
            _ => "EN",
        };

        flowchart.SetStringVariable("Language", currentLanguage);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
