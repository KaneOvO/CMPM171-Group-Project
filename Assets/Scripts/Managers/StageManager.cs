using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
[AddComponentMenu("Managers/PlayerStageManager")]
public class StageManager : MonoBehaviour
{
    private static StageManager _instance;
    public SaveData saveData => GameManager.Instance.saveData;
    [Header("Event Listener: Start Button Clicked Event.")]
    public VoidEventSO startClickedEvent;
    [Header("Event Listener: Player Activity Is Done Event.")]
    public VoidEventSO playerActivityDoneEvent;
    [Header("Event Sender: On Stage Moved.")]
    public VoidEventSO onStageMoved;
    public Stage currentStage => GameManager.Instance.saveData.currentStage;
    public static StageManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<StageManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
        transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
    }

    private void OnEnable()
    {
        startClickedEvent.onEventRaised += HandleGameStartEvent;
    }
    private void OnDisable()
    {
        startClickedEvent.onEventRaised -= HandleGameStartEvent;
    }
    private void HandleGameStartEvent()
    {
        StageMoved();
    }
    public void StageMoved()
    {
        GameManager.Instance.saveData.currentStage = currentStage switch
        {
            Stage.Morning => Stage.Noon,
            Stage.Noon => Stage.Afternoon,
            Stage.Afternoon => Stage.Night,
            Stage.Night => Stage.Morning,
            Stage.Default => Stage.Morning,
            _ => Stage.Default,
        };
        onStageMoved.RaiseEvent();
    }
}
