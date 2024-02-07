using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public Localization localizationComponent;
    public Flowchart flowchart;

    private string currentLanguage;

    void Awake()
    {
        localizationComponent = FindObjectOfType<Localization>();
        flowchart =  FindObjectOfType<Flowchart>();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    
    public void SetEnglish()
    {
        currentLanguage = "EN";
        flowchart.SetStringVariable("Language", currentLanguage);
        UpdateLanguage();
    }



    public void SetChinese()
    {
        currentLanguage = "";
        flowchart.SetStringVariable("Language", currentLanguage);
        UpdateLanguage();
    }

    public void UpdateLanguage()
    {
        localizationComponent.SetActiveLanguage(currentLanguage, true);
    }
}
