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

    //编写一个函数设置fungus插件本地化为英文
    public void SetEnglish()
    {
        currentLanguage = "English";
        flowchart.SetStringVariable("Language", currentLanguage);
        localizationComponent.SetActiveLanguage(currentLanguage, true);
    }



    public void SetChinese()
    {
        currentLanguage = "";
        flowchart.SetStringVariable("Language", currentLanguage);
        localizationComponent.SetActiveLanguage(currentLanguage, true);
    }
}
