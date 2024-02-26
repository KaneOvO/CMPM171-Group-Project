using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public class SaveData
{
    public Language currentLanguage;
    public int currentDay;
    public Stage currentStage;
    public PlayerState playerState;
    public List<InventoryItem> inventory;
    public List<TeachInfo> teachInfo;
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
public class TeachInfo : IComparable<TeachInfo>
{
    public string id;
    public bool isTeach;
    public TeachInfo(string id, bool isTeach)
    {
        this.id = id;
        this.isTeach = isTeach;
    }
    public int CompareTo(TeachInfo other)
    {
        return string.Compare(this.id, other.id);
    }
}

[System.Serializable]
public class PlayerState
{
    [Range(0, 3)] public int energy;
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
    public float sellValue;
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

