using System.Collections;
using System.Collections.Generic;
using Fungus;
using UnityEngine;

public class LanguageSwitcher : MonoBehaviour
{
    public Localization localizationComponent;

    void Awake()
    {
        localizationComponent = FindObjectOfType<Localization>();
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
        localizationComponent.SetActiveLanguage("English", true);
    }



    public void SetChinese()
    {
        localizationComponent.SetActiveLanguage("", true);
    }
}
