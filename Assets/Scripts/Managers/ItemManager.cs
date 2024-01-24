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
    public List<Item> items;
    public Item ID(string id)
    {
        return items.Find(x => x.id == id);
    }
}
