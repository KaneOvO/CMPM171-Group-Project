using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[AddComponentMenu("Managers/PlayerActivityManager")]
public class PlayerActivityManager : MonoBehaviour
{
    private static PlayerActivityManager _instance;
    public SaveData saveData => GameManager.Instance.saveData;
    public static PlayerActivityManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<PlayerActivityManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    [SerializeField]
    public List<BasicActivity> activityList = new List<BasicActivity>();
    private readonly List<BasicActivity> initializedActivityList = new List<BasicActivity>()
    {
        new Morning(),
        new PickAction(),
        new Noon(),
        new PickAction(),
        new Afternoon(),
        new PickAction(),
        new Night()
    };
    private BasicActivity currentActivity;
    private int currentActivityIndex = 0;
    public void Start()
    {
        InitializedActivityList();
        LoadActivity(currentActivityIndex);
    }
    private void InitializedActivityList()
    {
        activityList.Clear();
        activityList.AddRange(initializedActivityList);
    }

    private void LoadActivity(int currentActivityIndex)
    {
        currentActivityIndex = (int)Mathf.Clamp(currentActivityIndex, 0, activityList.Count - 1);
        currentActivity = activityList[currentActivityIndex];
        currentActivity.OnEnter();
    }
    public void NextActivity()
    {
        QuitActivity();
        currentActivityIndex++;
        LoadActivity(currentActivityIndex);
    }

    public void QuitActivity()
    {
        currentActivity.OnExit();
    }

    public void LoadActivity(BasicActivity basicActivity)
    {
        currentActivity = activityList.Find(x => x.id == basicActivity.id);
        if (currentActivity == null) Debug.Log($"Current activity {basicActivity.id} is not found");
        currentActivityIndex = currentActivity == null ? activityList.Count - 1 : activityList.FindIndex(x => x.id == basicActivity.id);
        LoadActivity(currentActivityIndex);
    }

    private void Update()
    {
        currentActivity.OnUpdate();
    }
    private void FixedUpdate()
    {
        currentActivity.OnFixedUpdate();
    }

    public void NewDay(){
        saveData.currentDay++;
        InitializedActivityList();
        currentActivityIndex = 0;
        LoadActivity(currentActivityIndex);
    }
}
