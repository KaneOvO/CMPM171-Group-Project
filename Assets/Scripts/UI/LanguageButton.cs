using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Button))]
public class LanguageButton : MonoBehaviour
{
    public Language language;
    [HideInInspector] Button button;
    private void OnEnable()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(LanguageButtonClicked);
    }
    public void LanguageButtonClicked()
    {
        GameManager.Instance.playerConfig.currentLanguage = language;
        UIManager.Instance.InvokeLanguageChangeEvents();
    }
    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }
}
