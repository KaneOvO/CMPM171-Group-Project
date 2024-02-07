using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
[AddComponentMenu("Managers/PlayerActivityManager")]
public class PlayerActivityManager : MonoBehaviour
{
    private static PlayerActivityManager _instance;
    public SaveData saveData => GameManager.Instance.saveData;
    public UnityEvent energyEmpty;
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
        transform.parent = GameObject.Find("Managers").transform;
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
        //  LoadActivity;
        LoadActivity();
    }
    public void LoadActivity()
    {
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
        // FindObjectOfType<GameManager>().OnJsonLoad += currentActivity.OnEnter;
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

    public void LoadActivity(string id)
    {
        currentActivity = activityList.Find(x => x.id == id);
        if (currentActivity == null) Debug.Log($"Current activity {id} is not found");
        currentActivityIndex = currentActivity == null ? activityList.Count - 1 : activityList.FindIndex(x => x.id == id);
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

    public void NewDay()
    {
        saveData.currentDay++;
        if (saveData.currentDay > GameManager.Instance.endDay) { LoadEnd(); return; }
        InitializedActivityList();
        currentActivityIndex = 0;
        LoadActivity(currentActivityIndex);
    }
    public void LoadEnd()
    {
        BasicActivity theEnd = new End();
        activityList.Add(theEnd);
        LoadActivity(theEnd);
    }

    public void SkippedActivity()
    {
        LoadActivity("Night");
    }
}
