using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Language
{
    English = 0,
    Chinese = 1,
    Japanese = 2,
}

public enum SceneType
{
    Opening = 0,
    Title = 1,
    Setting = 2,
    Credit = 3,
    InGame = 4,
    Ending = 5,
    Test = 6,
}
public enum Stage
{
    Morning = 0,
    Noon = 1,
    Afternoon = 2,
    Night = 3,
}

public enum InGameEventType
{
    Default = 0,
    Daily = 1,
    Special = 2,
}