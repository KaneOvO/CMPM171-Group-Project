using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;

public class Wholesale : MonoBehaviour
{
    public float totalCost;
    [Header("Canvas Script")]
    public WholesaleCanvas canvas;

    [Header("Events Sender: Load Scene")]
    public SceneLoadEventSO loadSceneEventSO;
    public AssetReference wholesaleEndScene;
    private int playerReputation => PlayerStateManager.Instance.playerState.reputation;
    public Button buyButton;

    void Start()
    {
        foreach (Item item in ItemManager.Instance.items)
        {
            if (playerReputation >= item.reputationRequired)
            {
                canvas.items.Add(item);
            }
        }
        canvas.CreateItemPrefabs();
        CalculateTotalCost();
    }

    public void CalculateTotalCost()
    {
        totalCost = 0f;
        foreach (Transform child in canvas.displayGrid)
        {
            PrefabController prefabController = child.GetComponent<PrefabController>();
            if (prefabController != null)
            {
                totalCost += prefabController.GetCost();
            }
            canvas.SetTotalCost(totalCost);
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
            foreach (Transform child in canvas.displayGrid)
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
