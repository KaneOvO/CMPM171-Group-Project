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
    [Header("Event Listener: On Energy Empty.")]
    public VoidEventSO energyEmpty;
    [Header("Event Listener: On Json Load.")]
    public VoidEventSO OnJsonLoad;
    [Header("Event Listener: Start Button Clicked Event.")]
    public VoidEventSO startClickedEvent;
    [Header("Event Listener: Player Activity Is Done Event.")]
    public VoidEventSO playerActivityDoneEvent;
    [Header("Event Sender: In Game Event Tirgger Checker.")]
    public VoidEventSO inGameEventTriggerChecker;
    private bool isAbleToStage = false;
    private BasicStage currentStage;
    private BasicStage nextStage;
    private BasicStage morning = new Morning();
    private BasicStage noon = new Noon();
    private BasicStage afternoon = new Afternoon();
    private BasicStage night = new Night();
    private BasicStage emptyStage = new EmptyStage();
    private int endDay => GameManager.Instance.inGameData.initialDatas.endDay;
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
        energyEmpty.onEventRaised += HandleEnergyEmpty;
        OnJsonLoad.onEventRaised += HandleJsonLoad;
        startClickedEvent.onEventRaised += HandleGameStartEvent;
        playerActivityDoneEvent.onEventRaised += HandlePlayerActivityDoneEvent;
    }
    private void OnDisable()
    {
        energyEmpty.onEventRaised -= HandleEnergyEmpty;
        OnJsonLoad.onEventRaised -= HandleJsonLoad;
        startClickedEvent.onEventRaised -= HandleGameStartEvent;
        playerActivityDoneEvent.onEventRaised -= HandlePlayerActivityDoneEvent;
    }
    private void HandleEnergyEmpty()
    {
        return;
    }
    private void HandleJsonLoad()
    {
        isAbleToStage = true;
    }
    private void HandleGameStartEvent()
    {
        LoadStage(DetermineNextStage());
    }
    private void HandlePlayerActivityDoneEvent()
    {
        LoadStage(DetermineNextStage());
    }
    public void Start()
    {
        currentStage = emptyStage;
        nextStage = emptyStage;
    }
    public void LoadStage(BasicStage activity)
    {
        currentStage?.OnExit();
        currentStage = activity;
        currentStage.OnEnter();
    }
    public void LoadNextStage()
    {
        currentStage?.OnExit();
        currentStage = nextStage;
        currentStage.OnEnter();
    }
    public void ExitCurrentStage()
    {
        currentStage?.OnExit();
    }
    public BasicStage DetermineNextStage()
    {
        return nextStage = (Stage)saveData.currentStage switch
        {
            Stage.Morning => noon,
            Stage.Noon => afternoon,
            Stage.Afternoon => night,
            Stage.Night => morning,
            Stage.Default => morning,
            _ => emptyStage,
        };
    }
    private void Update()
    {
        if (isAbleToStage) currentStage?.OnUpdate();
    }
    public void NewDay()
    {
        saveData.currentDay++;
        if (saveData.currentDay > endDay) { LoadEnd(); return; }
        LoadStage(DetermineNextStage());
    }
    public void LoadEnd()
    {
    }

    public void InGameEventCheck(){
        inGameEventTriggerChecker.RaiseEvent();
    }
}
