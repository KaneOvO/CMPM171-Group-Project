using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
[AddComponentMenu("Managers/ItemManager")]
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
        transform.parent = GameObject.FindWithTag("ManagersContainer").transform;
    }
    public List<Item> items { get { return GameManager.Instance.inGameData.items; } }
    public List<InventoryItem> inventory => GameManager.Instance.saveData.inventory;
    public Item ID(string id)
    {
        return items.Find(x => x.id == id);
    }
    public bool AddItemAmount(string id, int amount = 1)
    {
        if (inventory.Exists(x => x.id == id))
        {
            inventory.Find(x => x.id == id).amount += amount;
        }
        else
        {
            inventory.Add(new InventoryItem(id, amount));
        }
        return OrganizeInventory();
    }
    public bool AddItemAmount(List<InventoryItem> inventories)
    {
        foreach (var item in inventories)
        {
            if (inventory.Exists(x => x.id == item.id))
            {
                inventory.Find(x => x.id == item.id).amount += item.amount;
            }
            else
            {
                inventory.Add(item);
            }
        }
        return OrganizeInventory();
    }
    public bool SetItemAmount(string id, int amount = 0)
    {
        if (amount < 0) return false;
        if (!inventory.Exists(x => x.id == id)) return false;
        inventory.Find(x => x.id == id).amount = amount;
        return OrganizeInventory();
    }
    public bool OrganizeInventory()
    {
        List<InventoryItem> itemsToRemove = new List<InventoryItem>();

        foreach (var item in inventory)
        {
            item.amount = Math.Clamp(item.amount, 0, ID(item.id).maxStack);
            if (item.amount <= 0)
            {
                itemsToRemove.Add(item);
            }
        }

        foreach (var itemToRemove in itemsToRemove)
        {
            inventory.Remove(itemToRemove);
        }

        inventory.Sort();
        return true;
    }
    public GameObject MakeItemGameObject(Item item)
    {
        GameObject obj = new GameObject($"{item.id}");
        obj.AddComponent<SpriteRenderer>();
        obj.transform.parent = transform;
        Texture2D texture = Resources.Load<Texture2D>(item.spriteUrl);
        Sprite loadedSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.zero);
        obj.GetComponent<SpriteRenderer>().sprite = loadedSprite;
        return obj;
    }
}
