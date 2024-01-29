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
    public void AddItem(string id, int amount = 1)
    {
        if (inventory.ContainsKey(id))
        {
            inventory[id] += amount;
        }
        else
        {
            inventory.Add(id, amount);
        }
        OrganizeInventory();
    }
    public void OrganizeInventory()
    {
        foreach (var item in inventory)
        {
            inventory[item.Key] = (int)Mathf.Clamp(item.Value, 0, ID(item.Key).maxStack);
            if (inventory[item.Key] <= 0) inventory.Remove(item.Key);
        }
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
