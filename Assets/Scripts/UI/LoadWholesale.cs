using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LoadWhollesale : MonoBehaviour
{
    public int totalCost;
    public TMP_Text totalCostText;
    public TMP_Text moneyText;
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
                prefabController.setPanelInScene(panelInScene);

                prefabController.setItem(item);

            }
        }
    }

    void Start()
    {
        panelInScene = GameObject.Find("Description Panel");
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

    void Update()
    {
        moneyText.text = $"Money: ${GameManager.Instance.saveData.playerState.money}";
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
                    prefabController.count = 0;
                    prefabController.UpdateCounterText();
                }
            }
        }
        Debug.Log("Bought items:");
        foreach (var item in ItemManager.Instance.inventory)
        {
            Debug.Log(item.id + " " + item.amount);
        }
    }
}
