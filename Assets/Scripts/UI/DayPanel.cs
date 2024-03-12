using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class DayPanel : MonoBehaviour
{
    private TextMeshProUGUI dayTextMesh;
    private TextMeshProUGUI stageTextMesh;
    private TextMeshProUGUI targetTextMesh;
    public int currentDay => GameManager.Instance.saveData.currentDay;
    public int dayTextIndex;
    public List<Contents> localization => GameManager.Instance.inGameData.localization;
    public TMP_FontAsset font => UIManager.Instance.font;
    public List<int> StageTextList;
    private int stageTextIndex;
    public int targetTextIndex;
    private void OnEnable()
    {
        dayTextMesh = transform.Find("DayText").GetComponent<TextMeshProUGUI>();
        stageTextMesh = transform.Find("StageText").GetComponent<TextMeshProUGUI>();
        targetTextMesh = transform.Find("TargetText").GetComponent<TextMeshProUGUI>();
        UIManager.Instance.onLanguageChange.AddListener(Refresh);
        Refresh();
    }
    private void OnDisable()
    {
        UIManager.Instance.onLanguageChange.RemoveListener(Refresh);
    }

    public void Refresh()
    {
        DayTextRefresh();
        StageTextRefresh();
        TargetTextRefresh();
    }
    public void DayTextRefresh()
    {
        dayTextMesh.font = font;
        if (dayTextIndex >= localization.Count) return;
        List<string> contents = localization[dayTextIndex].contents;
        if (contents.Count <= 0) return;
        string content = contents[(int)GameManager.Instance.saveData.currentLanguage];
        dayTextMesh.text = $"{content}:{GameManager.Instance.saveData.currentDay}";
    }

    public void StageTextRefresh()
    {
        stageTextMesh.font = font;
        stageTextIndex = GameManager.Instance.saveData.currentStage switch
        {
            Stage.Morning => StageTextList[0],
            Stage.Afternoon => StageTextList[1],
            Stage.Night => StageTextList[2],
            _ => StageTextList[0],
        };
        if (stageTextIndex >= localization.Count) return;
        List<string> contents = localization[stageTextIndex].contents;
        if (contents.Count <= 0) return;
        string content = contents[(int)GameManager.Instance.saveData.currentLanguage];
        if (content != null) { stageTextMesh.text = content; }
    }

    public void TargetTextRefresh()
    {
        targetTextMesh.font = font;
        if (targetTextIndex >= localization.Count) return;
        List<string> contents = localization[targetTextIndex].contents;
        if (contents.Count <= 0) return;
        string content = contents[(int)GameManager.Instance.saveData.currentLanguage];
        if (content != null) { targetTextMesh.text = content; }
    }
}
