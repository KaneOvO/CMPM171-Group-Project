using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    private static ItemManager _instance;
    public static ItemManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ItemManager>();
            }
            return _instance;
        }
    }
    private void Awake()
    {
        _instance = this;
    }
    public List<Item> items { get { return GameManager.Instance.inGameData.items; } }
    public Item ID(string id)
    {
        return items.Find(x => x.id == id);
    }
    public Dictionary<string, int> playerItems => GameManager.Instance.saveData.playerItems;
    public void AddItem(string id, int amount = 1)
    {
        if (playerItems.ContainsKey(id))
        {
            playerItems[id] += amount;
        }
        else
        {
            playerItems.Add(id, amount);
        }
    }
}
