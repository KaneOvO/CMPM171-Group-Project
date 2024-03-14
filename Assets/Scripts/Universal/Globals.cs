using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveData
{
    public int currentDay;
    public Stage currentStage;
    public PlayerState playerState;
    public List<InventoryItem> inventory;
    public TeachInfo teachInfo;
    public EventInfo eventInfo;
}
[System.Serializable]
public class PlayerConfig
{
    public Language currentLanguage;
    public int volume;
}

[System.Serializable]
public class InventoryItem : IComparable<InventoryItem>
{
    public string id;
    public int amount;
    public InventoryItem(string id, int amount)
    {
        this.id = id;
        this.amount = amount;
    }
    public int CompareTo(InventoryItem other)
    {
        return string.Compare(this.id, other.id);
    }
}
[System.Serializable]
public class TeachInfo
{
    public bool purchase;
    public bool sale;
    public bool takeBreak;
    public bool volunteer;
    public bool dayLabor;
}
[System.Serializable]
public class EventInfo
{
    public bool cargo;
    public bool elder1;
    public bool interest1;
}

[System.Serializable]
public class PlayerState
{
    public bool isSick;
    public float money;
    [Range(0, 100)] public int health;
    [Range(0, 100)] public int reputation;
    [Range(0, 100)] public int moral;
}
[System.Serializable]
public class InGameData
{
    public List<Item> items;
    public InitialDatas initialDatas;
    public List<Contents> localization;
}
[System.Serializable]
public class Contents
{
    public List<string> contents;
}
[System.Serializable]
public class InitialDatas
{
    public PlayerState playerState;
    [Range(0, 30)] public int endDay;
    public int sick;
}

[System.Serializable]
public class Item
{
    public string id;
    public List<string> name;
    public int reputationRequired;
    public float originalPrice;
    public float sellPrice;
    public int sellValue;
    public int maxStack;
    public List<string> description;
    public string spriteUrl;
}

[System.Serializable]
public class InGameTime
{
    public int hour;
    public int minute;
}
[System.Serializable]
public class RealTime
{
    public int hour;
    public int minute;
    public int second;
}

[System.Serializable]
public class Achievement
{
    public string id { get; private set; }
    public string name;
    public string description;
    public bool isCompleted = false;
    public virtual bool Check() { return this.isCompleted = true; }
}
public static class Global
{
    public const int TEST_INT = 0;
}

