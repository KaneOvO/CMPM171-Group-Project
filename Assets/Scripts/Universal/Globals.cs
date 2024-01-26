using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData{
    public int currentDay;
    public PlayerState playerState;
    public Dictionary<string,int> playerItems;
}
[System.Serializable]
public class PlayerState
{
    public int energy;
    public bool isSick;
    public int money;
    public int health;
    public int reputation;
    public int moral;
}
[System.Serializable]
public class InGameData{
    public List<Item> items;
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