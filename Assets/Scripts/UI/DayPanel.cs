using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
public class DayPanel : MonoBehaviour
{
    private TMPLanguageSwitch dayText;
    private TMPLanguageSwitch stageText;
    public int currentDay => GameManager.Instance.saveData.currentDay;
    public List<int> StageTextList;

    private void Awake()
    {
        dayText = transform.Find("DayText").GetComponent<TMPLanguageSwitch>();
        stageText = transform.Find("StageText").GetComponent<TMPLanguageSwitch>();
    }
    private void OnEnable()
    {
        dayText.refreshDone.AddListener(HandleDayTextRefreshDone);
        stageText.refreshDone.AddListener(HandleStageTextRefreshDone);
    }
    private void OnDisable()
    {
        dayText.refreshDone.RemoveListener(HandleDayTextRefreshDone);
        stageText.refreshDone.RemoveListener(HandleStageTextRefreshDone);
    }
    public void HandleDayTextRefreshDone()
    {
        Debug.Log("DayText Refresh Done");
        dayText.textMesh.text = $"{GameManager.Instance.saveData.currentDay}/30 {dayText.textMesh.text}";
    }

    public void HandleStageTextRefreshDone()
    {
        Debug.Log("StageText Refresh Done");
        stageText.localizationIndex = GameManager.Instance.saveData.currentStage switch
        {
            Stage.Morning => StageTextList[0],
            Stage.Noon => StageTextList[1],
            Stage.Afternoon => StageTextList[2],
            _ => StageTextList[0],
        };
        if (stageText.localizationIndex >= stageText.localization.Count) return;
        List<string> contents = stageText.localization[stageText.localizationIndex].contents;
        if (contents.Count <= 0) return;
        string content = contents[(int)GameManager.Instance.saveData.currentLanguage];
        if (content != null) { stageText.textMesh.text = content; }
    }
}
