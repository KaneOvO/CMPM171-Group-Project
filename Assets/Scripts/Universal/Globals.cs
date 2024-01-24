using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class SaveData{
    public int currentDay;
    public PlayerState playerState;
}
[System.Serializable]
public class PlayerState
{
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
    public int maxStack;
    public List<string> description;
    public string spriteUrl;
}