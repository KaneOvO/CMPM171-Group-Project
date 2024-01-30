using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    public uint currentDay;
    public PlayerState playerState;
    public Dictionary<string, int> inventory;
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