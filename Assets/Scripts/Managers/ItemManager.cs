using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
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
    }
    public List<Item> items { get { return GameManager.Instance.inGameData.items; } }
    public Dictionary<string, int> inventory => GameManager.Instance.saveData.inventory;
    public Item ID(string id)
    {
        return items.Find(x => x.id == id);
    }
    public bool AddItemAmount(string id, int amount = 1)
    {
        if (inventory.ContainsKey(id))
        {
            inventory[id] += amount;
        }
        else
        {
            inventory.Add(id, amount);
        }
        return OrganizeInventory();
    }
    public bool AddItemAmount(Dictionary<string, int> itemDic)
    {
        foreach (var item in itemDic)
        {
            if (inventory.ContainsKey(item.Key))
            {
                inventory[item.Key] += item.Value;
            }
            else
            {
                inventory.Add(item.Key, item.Value);
            }
        }
        return OrganizeInventory();
    }
    public bool SetItemAmount(string id, int amount = 0)
    {
        if (amount < 0) return false;
        if (!inventory.ContainsKey(id)) return false;
        inventory[id] = amount;
        return OrganizeInventory();
    }
    public bool OrganizeInventory()
    {
        foreach (var item in inventory)
        {
            inventory[item.Key] = (int)Mathf.Clamp(item.Value, 0, ID(item.Key).maxStack);
            if (inventory[item.Key] <= 0) inventory.Remove(item.Key);
        }
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
