using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Wholesale : MonoBehaviour
{
    public int totalCost;
    public TMP_Text totalCostText;
    public TMP_Text moneyText;
    public GameObject itemPrefab;
    public Transform parentForPrefabs;
    public GameObject panelInScene;

    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadSceneEventSO;
    public AssetReference wholesaleEndScene;
    private int playerMoral => PlayerStateManager.Instance.playerState.moral;
    public Button buyButton;
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
                prefabController.wholesale = this;
            }
        }
    }

    void Start()
    {
        panelInScene = GameObject.Find("Description Panel");
        panelInScene.SetActive(false);
        List<Item> buyableItems = new List<Item>();
        foreach (Item item in ItemManager.Instance.items)
        {
            if (playerMoral >= item.moralRequired)
            {
                buyableItems.Add(item);
            }
        }
        CreateItemPrefabs(buyableItems);

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
        buyButton.interactable = GameManager.Instance.saveData.playerState.money >= totalCost;
    }

    public void BuyItems()
    {
        CalculateTotalCost();
        if (GameManager.Instance.saveData.playerState.money < totalCost)
        {
            Debug.Log("Not enough money");
            return;
        }
        else
        {
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

        loadSceneEventSO.RaiseEvent(wholesaleEndScene, true);
    }
}
