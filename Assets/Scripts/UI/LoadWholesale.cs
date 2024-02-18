using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadWhollesale : MonoBehaviour
{
    public int totalCost;
    public TMP_Text totalCostText;
    public GameObject itemPrefab;
    public Transform parentForPrefabs;
    public GameObject panelInScene;

    public void CreateItemPrefabs(List<Item> items)
    {
        foreach (Item item in items)
        {
            // Instantiate the prefab
            GameObject newItemPrefab = Instantiate(itemPrefab, parentForPrefabs);
            newItemPrefab.name = item.id;
            PrefabController prefabController = newItemPrefab.GetComponent<PrefabController>();
            if (prefabController != null)
            {
                // Set the item image
                // prefabController.SetItemImage(item.spriteUrl);

                // // Set the item cost
                // prefabController.SetItemCost(item.price[0]);

                // //Set the panel
                prefabController.setPanelInScene(panelInScene);

                prefabController.setItem(item);

            }
        }
    }

    void Start()
    {
        // Debug.Log(GameManager.Instance.saveData.currentLanguage);
        panelInScene = GameObject.Find("Description Panel");
        // if (panelInScene == null)
        // {
        //     Debug.LogError("Panel not found in the scene. Please check the name and ensure it is active.");
        //     return;
        // }
        // else{
        // 
        // }
        panelInScene.SetActive(false);


        Item itemList1 = ItemManager.Instance.ID("00");
        Item itemList2 = ItemManager.Instance.ID("01");
        Item itemList3 = ItemManager.Instance.ID("02");
        List<Item> items = new List<Item>{
            itemList1,
            itemList2,
            // itemList3,
        };
        CreateItemPrefabs(items);
        CalculateTotalCost();
    }

    public void CalculateTotalCost()
    {
        totalCost = 0;
        foreach (Transform child in parentForPrefabs)
        {
            PrefabController prefabController = child.GetComponent<PrefabController>();
            if (prefabController != null)
            {
                totalCost += prefabController.GetCost();
            }
            totalCostText.text = $"Total Cost: ${totalCost}";
        }
    }

    public void BuyItems()
    {
        CalculateTotalCost();
        if (GameManager.Instance.saveData.playerState.money < totalCost)
        {
            Debug.Log("Not enough money");
            return;
        }else{
            GameManager.Instance.saveData.playerState.money -= totalCost;
            foreach (Transform child in parentForPrefabs)
            {
                PrefabController prefabController = child.GetComponent<PrefabController>();
                if (prefabController != null)
                {
                    ItemManager.Instance.AddItemAmount(prefabController.name, prefabController.count);
                }
            }
        }
        // Debug.Log("Bought items:");
        // foreach (var item in ItemManager.Instance.inventory)
        // {
        //     Debug.Log(item.id + " " + item.amount);
        // }
    }
}
