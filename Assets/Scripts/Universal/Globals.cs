using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public Language currentLanguage;
    public int currentDay;
    public Stage currentStage;
    public PlayerState playerState;
    public List<InventoryItem> inventory;
}

[System.Serializable]
public class InventoryItem
{
    public string id;
    public int amount;
    public InventoryItem(string id, int amount)
    {
        this.id = id;
        this.amount = amount;
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
    public List<float> price;
    public float sellRate;
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

public static class Global
{
    public const int TEST_INT = 0;
}