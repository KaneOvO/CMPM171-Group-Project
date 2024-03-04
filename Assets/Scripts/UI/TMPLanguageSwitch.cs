using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
[RequireComponent(typeof(TMP_Text))]
public class TMPLanguageSwitch : MonoBehaviour
{
    [HideInInspector] public TextMeshProUGUI textMesh;
    public int localizationIndex = 0;
    public List<Contents> localization => GameManager.Instance.inGameData.localization;
    public TMP_FontAsset font => UIManager.Instance.font;
    void OnEnable()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        UIManager.Instance.onLanguageChange.AddListener(Refresh);
        Refresh();
    }
    void OnDisable()
    {
        UIManager.Instance.onLanguageChange.RemoveListener(Refresh);
    }
    public void Refresh()
    {
        textMesh.font = font;
        if (localizationIndex >= localization.Count) return;
        List<string> contents = localization[localizationIndex].contents;
        if (contents.Count <= 0) return;
        string content = contents[(int)GameManager.Instance.saveData.currentLanguage];
        if (content != null) { textMesh.text = content; }
    }
}
